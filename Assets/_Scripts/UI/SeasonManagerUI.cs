using UnityEngine;
using UnityEngine.UI;

public class SeasonManagerUI : MonoBehaviour
{
    public Text seasonText;

    private void Start()
    {
        if (SeasonManager.Instance != null)
        {
            UpdateSeasonUI(SeasonManager.Instance.CurrentSeason);
            SeasonManager.Instance.OnSeasonChanged += UpdateSeasonUI;
        }
    }

    private void OnDestroy()
    {
        if (SeasonManager.Instance != null)
        {
            SeasonManager.Instance.OnSeasonChanged -= UpdateSeasonUI;
        }
    }

    private void UpdateSeasonUI(Season season)
    {
        if (seasonText != null) seasonText.text = "Season: " + season.ToString();
    }
}
