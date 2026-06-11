using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverSpawner : MonoBehaviour
{
    [Header("Diver Prefabs")]
    public List<GameObject> diverPrefabs = new List<GameObject>();

    [Header("Spawn Range")]
    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    void Start()
    {
        SpawnRandomDiver();
    }

    void SpawnRandomDiver()
    {
        if (diverPrefabs.Count == 0) return;

        // ch?n diver ng?u nhiên
        GameObject diverPrefab = diverPrefabs[Random.Range(0, diverPrefabs.Count)];

        // v? trí ng?u nhiên
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(x, y, 0);

        Instantiate(diverPrefab, spawnPos, Quaternion.identity);
    }
}