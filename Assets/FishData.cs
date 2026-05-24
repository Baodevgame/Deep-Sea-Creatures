using UnityEngine;

[System.Serializable]
public class FishData
{
    public string fishName;
    public GameObject prefab;

    public Season[] seasons;

    [Range(1, 5)]
    public int rarity;

    [Range(0, 100)]
    public float spawnWeight;
}