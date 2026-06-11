using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public List<TreasureLayer> layers;

    public int maxTotalTreasure = 5;

    public float minX = -2.5f;
    public float maxX = 2.5f;

    void Start()
    {
        SpawnTreasures();
    }

    void SpawnTreasures()
    {
        if (layers.Count == 0) return;

        int totalSpawned = 0;

        while (totalSpawned < maxTotalTreasure)
        {
            TreasureLayer layer = layers[Random.Range(0, layers.Count)];

            int rarity = RollRarity(layer);

            List<TreasureData> filtered = layer.treasures.FindAll(t => t.rarity == rarity);

            if (filtered.Count == 0) filtered = layer.treasures;

            TreasureData selected = GetByWeight(filtered);

            float x = Random.Range(minX, maxX);
            float y = -Random.Range(layer.minDepth, layer.maxDepth);

            Instantiate(selected.prefab, new Vector3(x, y, 0), Quaternion.identity);

            totalSpawned++;
        }
    }

    int RollRarity(TreasureLayer layer)
    {
        float total = 0;
        foreach (var r in layer.rarityWeights) total += r.weight;

        float rand = Random.Range(0, total);

        float sum = 0;
        foreach (var r in layer.rarityWeights)
        {
            sum += r.weight;
            if (rand < sum) return r.rarity;
        }

        return 1;
    }

    TreasureData GetByWeight(List<TreasureData> list)
    {
        float total = 0;
        foreach (var t in list) total += t.spawnWeight;

        float rand = Random.Range(0, total);

        float sum = 0;
        foreach (var t in list)
        {
            sum += t.spawnWeight;
            if (rand < sum) return t;
        }

        return list[0];
    }
}