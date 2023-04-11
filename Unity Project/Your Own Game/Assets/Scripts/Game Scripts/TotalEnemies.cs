using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalMaxEnemies;
    private int enemiesRemaining;
    private int enemiesSpawned = 0;
    private bool spawnEnemy = true;

    void Start()
    {
        enemiesRemaining = totalMaxEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned >= totalMaxEnemies)
        {
            spawnEnemy = false;
        }
    }

    public bool CanSpawnEnemy()
    {
        return spawnEnemy;
    }

    public void SpawnEnemy()
    {
        enemiesSpawned++;
    }

    public void RemoveEnemy()
    {
        enemiesSpawned--;
        enemiesRemaining--;
    }

    public int GetEnemiesRemaining()
    {
        return enemiesSpawned;
    }
}
