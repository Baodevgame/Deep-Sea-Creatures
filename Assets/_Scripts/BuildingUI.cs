using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [Header("Info")]
    public string buildingId;

    [Header("Level")]
    public int level = 1;
    public int maxLevel = 5;

    [Header("Cost")]
    public int baseCost = 1000;

    [Header("UI")]
    public Text levelText;
    public Text costText;

    private void Start()
    {
        Load();

        RefreshUI();
    }

    public void Upgrade()
    {
        if (level >= maxLevel)
            return;

        int cost = GetCost();

        if (!CurrencyManager.Instance.UsePearl(cost))
        {
            Debug.Log("Not enough pearl");
            return;
        }

        level++;

        Save();

        RefreshUI();

        OceanVillageManager.Instance.CheckCurrentAreaCompleted();
    }

    public int GetCost()
    {
        return baseCost * level;
    }

    void RefreshUI()
    {
        if (levelText != null)
            levelText.text = "Lv." + level;

        if (costText != null)
        {
            if (level >= maxLevel)
                costText.text = "MAX";
            else
                costText.text = GetCost().ToString();
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt(buildingId + "_Level", level);
        PlayerPrefs.Save();
    }

    void Load()
    {
        level = PlayerPrefs.GetInt(buildingId + "_Level", level);
    }
}