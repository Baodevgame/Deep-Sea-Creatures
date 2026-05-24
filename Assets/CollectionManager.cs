using UnityEngine;

public static class CollectionManager
{
    private const string DISCOVER_PREFIX = "COLLECTION_DISCOVERED_";
    private const string REWARD_PREFIX = "COLLECTION_REWARD_";

    // danh dau da phat hien
    public static void MarkDiscovered(CollectionType type)
    {
        string key = DISCOVER_PREFIX + type.ToString();
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    // kiem tra xem da duoc phat hien chua
    public static bool IsDiscovered(CollectionType type)
    {
        string key = DISCOVER_PREFIX + type.ToString();
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    // kiem tra xem da nhan thuong chua
    public static bool IsRewardClaimed(CollectionType type)
    {
        string key = REWARD_PREFIX + type.ToString();
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    // danh dau da nhan thuong
    public static void ClaimReward(CollectionType type)
    {
        string key = REWARD_PREFIX + type.ToString();
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    // Reset (dung de test)
    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}