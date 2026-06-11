using UnityEngine;

public class OceanVillageManager : MonoBehaviour
{
    public static OceanVillageManager Instance;

    [Header("Areas")]
    public GameObject[] areas;

    [Header("Current")]
    public int currentArea;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        currentArea =
            PlayerPrefs.GetInt("CurrentArea", 0);

        ShowCurrentArea();
    }

    public void ShowCurrentArea()
    {
        for (int i = 0; i < areas.Length; i++)
        {
            areas[i].SetActive(i == currentArea);
        }
    }

    public void CheckCurrentAreaCompleted()
    {
        BuildingUI[] buildings =
            areas[currentArea]
            .GetComponentsInChildren<BuildingUI>();

        foreach (BuildingUI building in buildings)
        {
            if (building.level < building.maxLevel)
                return;
        }

        UnlockNextArea();
    }

    void UnlockNextArea()
    {
        if (currentArea >= areas.Length - 1)
            return;

        currentArea++;

        PlayerPrefs.SetInt(
            "CurrentArea",
            currentArea);

        PlayerPrefs.Save();

        ShowCurrentArea();

        Debug.Log("Unlocked Area " + currentArea);
    }
}