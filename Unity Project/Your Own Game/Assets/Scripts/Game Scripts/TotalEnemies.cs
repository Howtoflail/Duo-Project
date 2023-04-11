using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalMaxEnemies;
    private int enemiesRemaining;
    private int enemiesSpawned = 0;
    void Start()
    {
        enemiesRemaining = totalMaxEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RemoveEnemy()
    {
        enemiesRemaining--;
    }
    public void SpawnEnemy()
    {
        enemiesSpawned++;
    }

    public int GetEnemiesRemaining()
    {
        return enemiesRemaining;
    }
}
