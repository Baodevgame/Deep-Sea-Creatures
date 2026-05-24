using UnityEngine;
using UnityEngine.UI;
using System;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

public class SeasonManager : MonoBehaviour
{
    public static SeasonManager Instance;

    public Season CurrentSeason { get; private set; }

    public event Action<Season> OnSeasonChanged;

    private DateTime startDate = new DateTime(2026, 1, 1);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateSeason();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void UpdateSeason()
    {
        DateTime now = DateTime.Now;

        int daysPassed = (now - startDate).Days;
        int weekIndex = daysPassed / 7;
        int seasonIndex = weekIndex % 4;

        Season newSeason = (Season)seasonIndex;

        if (newSeason != CurrentSeason)
        {
            CurrentSeason = newSeason;
            OnSeasonChanged?.Invoke(CurrentSeason);
        }
    }
}
