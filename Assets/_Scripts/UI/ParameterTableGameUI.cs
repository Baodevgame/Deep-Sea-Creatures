using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterTableGameUI : MonoBehaviour
{
    [SerializeField]private HookCatchFish hookCatchFish;

    public Text rodParameter;
    public Text hookParameter;
    public Text sinkerParameter;

    private void Start()
    {
        hookCatchFish = FindObjectOfType<HookCatchFish>(true);
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float max = FishingGearManager.Instance.maxLineLength;
        int maxSlot = FishingGearManager.Instance.hookSlots;
        float current = FishingGearManager.Instance.currentDepth;

        int currentFish = 0;

        if (hookCatchFish != null) currentFish = hookCatchFish.CurrentCatchCount;

        rodParameter.text = current.ToString("0") + " / " + max + " m";
        hookParameter.text = currentFish + " / " + maxSlot;
        sinkerParameter.text = FishingGearManager.Instance.fallSpeed.ToString("0.0");
    }
}
