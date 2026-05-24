using UnityEngine;

public static class FishingSaveSystem
{
    public static void Save()
    {
        PlayerPrefs.SetFloat("Rod_Length", FishingGearManager.Instance.maxLineLength);
        PlayerPrefs.SetInt("Rod_Level", FishingGearManager.Instance.rodLevel);

        PlayerPrefs.SetFloat("Sinker_Speed", FishingGearManager.Instance.fallSpeed);
        PlayerPrefs.SetInt("Sinker_Level", FishingGearManager.Instance.sinkerLevel);

        PlayerPrefs.SetInt("Hook_Slots", FishingGearManager.Instance.hookSlots);
        PlayerPrefs.SetInt("Hook_Level", FishingGearManager.Instance.hookLevel);

        PlayerPrefs.Save();
    }

    public static void Load()
    {
        FishingGearManager.Instance.maxLineLength = PlayerPrefs.GetFloat("Rod_Length", FishingGearManager.Instance.maxLineLength);
        FishingGearManager.Instance.rodLevel = PlayerPrefs.GetInt("Rod_Level", FishingGearManager.Instance.rodLevel);

        FishingGearManager.Instance.fallSpeed = PlayerPrefs.GetFloat("Sinker_Speed", FishingGearManager.Instance.fallSpeed);
        FishingGearManager.Instance.sinkerLevel = PlayerPrefs.GetInt("Sinker_Level", FishingGearManager.Instance.sinkerLevel);

        FishingGearManager.Instance.hookSlots = PlayerPrefs.GetInt("Hook_Slots", FishingGearManager.Instance.hookSlots);
        FishingGearManager.Instance.hookLevel = PlayerPrefs.GetInt("Hook_Level", FishingGearManager.Instance.hookLevel);
    }
}
