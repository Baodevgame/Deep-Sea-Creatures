using UnityEngine;

[System.Serializable]
public class TreasureData
{
    public string name;
    public GameObject prefab;

    [Range(1, 3)]
    public int rarity;

    public float spawnWeight;
}