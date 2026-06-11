using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ================= SAVE =================
    public void SaveAll()
    {
        PlayerPrefs.SetInt("Pearl", CurrencyManager.Instance.pearl);
        PlayerPrefs.SetInt("GachaTicket", CurrencyManager.Instance.gachaTicket);
        PlayerPrefs.SetInt("VipTicket", CurrencyManager.Instance.vipTicket);
        PlayerPrefs.SetInt("FisherReputation", CurrencyManager.Instance.fisherReputation);

        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

    // ================= LOAD =================
    public void LoadAll()
    {
        CurrencyManager.Instance.pearl = PlayerPrefs.GetInt("Pearl", 1000);
        CurrencyManager.Instance.gachaTicket = PlayerPrefs.GetInt("GachaTicket", 5);
        CurrencyManager.Instance.vipTicket = PlayerPrefs.GetInt("VipTicket", 1);
        CurrencyManager.Instance.fisherReputation = PlayerPrefs.GetInt("FisherReputation", 0);

        Debug.Log("Game Loaded");
    }
}