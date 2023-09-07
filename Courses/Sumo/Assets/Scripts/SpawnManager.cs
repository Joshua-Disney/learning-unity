using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9.0f;
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPos(), powerupPrefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {   enemyCount = FindObjectsOfType<Enemy>().Length;
            if (enemyCount == 0) {
                waveNumber++;
                if ( waveNumber % bossRound == 0 ) {
                    SpawnBossWave(waveNumber);
                } else {
                    SpawnEnemyWave(waveNumber);
                }
                int randomPowerup = Random.Range(0, powerupPrefabs.Length);
                Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPos(), powerupPrefabs[randomPowerup].transform.rotation);
            }   
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn) {
        for (int i = 0; i < enemiesToSpawn; i++) {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPos(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = 0.6f;
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        return spawnPos;
    }

    void SpawnBossWave(int currentRound) 
    {
        int miniEnemiesToSpawn;
        if (bossRound != 0) {
            miniEnemiesToSpawn = currentRound / bossRound;
        } else {
            miniEnemiesToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateSpawnPos(), bossPrefab.transform.rotation);
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++) {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPos(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
