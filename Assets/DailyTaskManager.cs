using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
    public static DailyTaskManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ResetIfNewDay();
    }

    public bool IsClaimed(string id)
    {
        return PlayerPrefs.GetInt(id, 0) == 1;
    }

    public void SetClaimed(string id)
    {
        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();
    }

    void ResetIfNewDay()
    {
        string today = System.DateTime.Now.ToString("yyyyMMdd");

        if (PlayerPrefs.GetString("LastDate") != today)
        {

            // ? ch? reset task
            PlayerPrefs.SetString("LastDate", today);

            Debug.Log("Reset Daily Task");
        }
    }
}