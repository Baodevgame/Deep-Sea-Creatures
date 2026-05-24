using UnityEngine;

[System.Serializable]
public class RarityWeight
{
    [Range(1, 5)]
    public int rarity;

    public float weight;
}