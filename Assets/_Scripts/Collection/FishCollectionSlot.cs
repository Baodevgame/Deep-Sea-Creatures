using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FishCollectionSlot : MonoBehaviour
{
    [Header("Fish Info")]
    [SerializeField] private CollectionType collectionType;

    [Header("UI")]
    [SerializeField] private Image collectionImage; 

    [SerializeField] private int reward;
    [SerializeField] private RewardEffectSimple effect;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        bool discovered = CollectionManager.IsDiscovered(collectionType);
        bool claimed = CollectionManager.IsRewardClaimed(collectionType);

        if (!discovered)
        {
            // Chua phat hien
            collectionImage.color = Color.black;
            button.interactable = false;
        }
        else if (!claimed)
        {
            // Da phat hien - chua nhan thuong
            collectionImage.color = Color.white;
            button.interactable = true;
        }
        else
        {
            // Da nhan thuong
            collectionImage.color = Color.white;
            button.interactable = false;
        }
        Debug.Log("Slot: " + gameObject.name + " Type: " + collectionType);
    }

    private void OnClick()
    {
        if (!CollectionManager.IsDiscovered(collectionType))
            return;

        if (CollectionManager.IsRewardClaimed(collectionType))
            return;

        GiveReward();
        AudioManager.Instance.PLayCollectionSFX();
        CollectionManager.ClaimReward(collectionType);
        UpdateVisual();
    }

    private void GiveReward()
    {
        effect.Play(GetComponent<RectTransform>(), reward);
        CurrencyManager.Instance.AddPearl(reward);
        CurrencyManager.Instance.AddFisherReputation(reward);
        Debug.Log("Reward point: " + reward);
    }
}