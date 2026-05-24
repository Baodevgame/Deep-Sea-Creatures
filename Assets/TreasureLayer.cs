using System.Collections.Generic;

[System.Serializable]
public class TreasureLayer
{
    public float minDepth;
    public float maxDepth;

    public int maxTreasure; 

    public List<TreasureData> treasures;

    public List<RarityWeight> rarityWeights;
}