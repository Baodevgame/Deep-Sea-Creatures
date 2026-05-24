using UnityEngine;

public enum RewardType
{
    Pearl,
    GachaTicket,
    VipTicket
}

[System.Serializable]
public class GachaItem
{
    public string itemName;
    public RewardType rewardType;
    public int amount;

    public float weight = 10f; 
    public Sprite icon;
}