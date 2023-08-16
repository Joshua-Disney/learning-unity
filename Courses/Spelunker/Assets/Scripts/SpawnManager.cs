using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject treasurePrefab;
    private int treasureCount;
    private float spawnRange = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++;) {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
            Instantiate(treasurePrefab, GenerateSpawnPos(), treasurePrefab.transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        treasureCount = FindObjectsOfType<Treasure>().Length;
        if (treasureCount == 0) {
            Debug.Log("YOU FOUND ALL THE TREASURE!!!");
        }
    }

    private Vector3 GenerateSpawnPos() {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = 0.5f;
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        return spawnPos;
    }
}
