using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParameterTableUI : MonoBehaviour
{
    public Text rodParameter;
    public Text hookParameter;
    public Text sinkerParameter;

    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        float max = FishingGearManager.Instance.maxLineLength;
        int maxSlot = FishingGearManager.Instance.hookSlots;

        rodParameter.text = max + " m";
        hookParameter.text = maxSlot.ToString();
        sinkerParameter.text = FishingGearManager.Instance.fallSpeed.ToString("0.0");
    }
}
