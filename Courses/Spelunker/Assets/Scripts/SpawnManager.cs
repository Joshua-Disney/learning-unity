using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject treasurePrefab;
    private float spawnRange = 20.0f;
    private int numOfPrefabs = 3;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPrefabs(numOfPrefabs);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 GenerateSpawnPos() {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = 0.5f;
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        return spawnPos;
    }

    void SpawnPrefabs(int prefabssToSpawn) {
        for (int i = 0; i < prefabssToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
            Instantiate(treasurePrefab, GenerateSpawnPos(), treasurePrefab.transform.rotation);
        }
    }
}
