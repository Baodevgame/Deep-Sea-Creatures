using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int pearl = 1000;
    public int gachaTicket = 5;
    public int vipTicket = 1;
    public int fisherReputation = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        SaveManager.Instance.LoadAll();
    }

    // ===== PEARL =====
    public void AddPearl(int amount)
    {
        pearl += amount;
        SaveManager.Instance.SaveAll();
        Debug.Log("?? Pearl = " + pearl);
    }

    public bool UsePearl(int amount)
    {
        if (pearl < amount) return false;
        pearl -= amount;
        SaveManager.Instance.SaveAll();
        return true;
    }

    // ===== TICKET =====
    public bool UseGachaTicket(int amount)
    {
        if (gachaTicket < amount) return false;
        gachaTicket -= amount;
        SaveManager.Instance.SaveAll();
        return true;
    }

    public void AddGachaTicket(int amount)
    {
        gachaTicket += amount;
        SaveManager.Instance.SaveAll();
    }
    public bool UseVipTicket(int amount)
    {
        if (vipTicket < amount) return false;
        vipTicket -= amount;
        SaveManager.Instance.SaveAll();
        return true;
    }

    public void AddVipTicket(int amount)
    {
        vipTicket += amount;
        SaveManager.Instance.SaveAll();
    }

    // =====Fisher Reputation=====

    public void AddFisherReputation(int amount)
    {
        fisherReputation += amount;
        SaveManager.Instance.SaveAll();
    }
}