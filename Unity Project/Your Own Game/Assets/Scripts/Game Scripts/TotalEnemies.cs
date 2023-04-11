using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalMaxEnemies;
    private int enemiesRemaining;
    private int enemiesSpawned = 0;
    private bool spawnEnemy = true;
    
    private int enemiesKilled = 0;

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

        if(enemiesKilled >= totalMaxEnemies)
        {
            SceneManager.LoadScene("Game End");
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
        enemiesKilled++;
    }

    public int GetEnemiesRemaining()
    {
        return enemiesSpawned;
    }
}
