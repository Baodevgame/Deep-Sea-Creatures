using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    public List<DepthLayer> depthLayers;

    public float leftX = -2.5f;
    public float rightX = 2.5f;

    void Start()
    {
        SpawnAllLayers();
    }

    void SpawnAllLayers()
    {
        foreach (var layer in depthLayers)
        {
            SpawnLayer(layer);
        }
    }

    void SpawnLayer(DepthLayer layer)
    {
        Season currentSeason = SeasonManager.Instance.CurrentSeason;

        List<FishData> validFish = new List<FishData>();

        foreach (var fish in layer.fishes)
        {
            if (fish.seasons != null &&
                System.Array.Exists(fish.seasons, s => s == currentSeason))
            {
                validFish.Add(fish);
            }
        }

        if (validFish.Count == 0) return;

        List<Vector2> spawnedPositions = new List<Vector2>();

        float minDistance = 3f; // khoang cach toi thieu giua ca

        for (int i = 0; i < layer.maxFishCount; i++)
        {
            Vector2 randomPos;
            bool validPosition;
            int attempts = 0;

            do
            {
                float randomX = Random.Range(leftX, rightX);
                float randomY = -Random.Range(layer.minDepth, layer.maxDepth);

                randomPos = new Vector2(randomX, randomY);
                validPosition = true;

                foreach (var pos in spawnedPositions)
                {
                    if (Vector2.Distance(pos, randomPos) < minDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }

                attempts++;
            }
            while (!validPosition && attempts < 20);

            if (!validPosition) continue;

            spawnedPositions.Add(randomPos);

            FishData selectedFish = GetRandomFish(validFish, layer);

            Instantiate(selectedFish.prefab,new Vector3(randomPos.x, randomPos.y, 0),Quaternion.identity);
        }
    }

    FishData GetRandomFishByWeight(List<FishData> fishList)
    {
        float totalWeight = 0;

        foreach (var fish in fishList) totalWeight += fish.spawnWeight;

        float randomValue = Random.Range(0, totalWeight);

        float currentWeight = 0;

        foreach (var fish in fishList)
        {
            currentWeight += fish.spawnWeight;

            if (randomValue < currentWeight) return fish;
        }

        return fishList[0];
    }
    int RollRarity(DepthLayer layer)
    {
        if (layer.rarityWeights == null || layer.rarityWeights.Count == 0) return 1;

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
    FishData GetRandomFish(List<FishData> fishList, DepthLayer layer)
    {
        int rarity = RollRarity(layer);

        List<FishData> filtered = fishList.FindAll(f => f.rarity == rarity);

        if (filtered.Count == 0) filtered = fishList;

        return GetRandomFishByWeight(filtered);
    }
}
