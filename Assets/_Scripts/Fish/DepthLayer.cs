using System.Collections.Generic;

[System.Serializable]
public class DepthLayer
{
    public float minDepth;
    public float maxDepth;

    public int maxFishCount;

    public List<FishData> fishes;

    public List<RarityWeight> rarityWeights;
}