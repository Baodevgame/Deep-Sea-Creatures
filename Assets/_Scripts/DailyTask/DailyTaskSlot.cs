using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskSlot : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private string taskID; 
    [SerializeField] private int requiredPoint; 

    [Header("Reward")]
    [SerializeField] private List<RewardData> rewards;

    [Header("UI")]
    [SerializeField] private Button claimButton;
    [SerializeField] private Text descriptionText;

    private void Start()
    {
        claimButton.onClick.AddListener(OnClaim);
        UpdateUI();
    }

    void UpdateUI()
    {
        int currentPoint = CurrencyManager.Instance.fisherReputation;

        descriptionText.text = currentPoint +"/"+ $"{requiredPoint}";

        if (DailyTaskManager.Instance.IsClaimed(taskID))
        {
            claimButton.interactable = false;
        }
        else if (currentPoint >= requiredPoint)
        {
            claimButton.interactable = true;
        }
        else
        {
            claimButton.interactable = false;
        }
    }

    public void OnClaim()
    {
        if (DailyTaskManager.Instance.IsClaimed(taskID)) return;

        int currentPoint = CurrencyManager.Instance.fisherReputation;
        if (currentPoint < requiredPoint) return;

        GiveReward();

        DailyTaskManager.Instance.SetClaimed(taskID);

        UpdateUI();
    }

    void GiveReward()
    {
        foreach (var reward in rewards)
        {
            Debug.Log($"Give {reward.type} x{reward.amount}");

            switch (reward.type)
            {
                case RewardType.Pearl:
                    CurrencyManager.Instance.AddPearl(reward.amount);
                    break;

                case RewardType.GachaTicket:
                    CurrencyManager.Instance.AddGachaTicket(reward.amount);
                    break;

                case RewardType.VipTicket:
                    CurrencyManager.Instance.AddVipTicket(reward.amount);
                    break;
            }
        }
    }
}