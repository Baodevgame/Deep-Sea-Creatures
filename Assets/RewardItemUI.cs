using UnityEngine;
using UnityEngine.UI;

public class RewardItemUI : MonoBehaviour
{
    public Image icon;
    public Text amountText;

    public void Setup(GachaItem item)
    {
        if (icon != null) icon.sprite = item.icon;

        if (amountText != null) amountText.text = "x" + item.amount.ToString();
    }
}