using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //PUBLIC
    // Start is called before the first frame update

    //Serializable Fields
    [SerializeField]
    private bool startSpawnOnStart;
    [SerializeField]
    public int totalMaxSpawns;

    [SerializeField]
    GameObject prefabEnemy;

    [SerializeField]
    int countMaxEnemies;

    [SerializeField]
    private float delaySpawn;

    [SerializeField]
    private Vector3 centerOfBox;

    [SerializeField]
    public float boxHeight,
        boxWidth,
        boxLength;

    [SerializeField]
    private float startSpawnDelay;

    //PRIVATE
    private GameObject managerObject;
    private TotalEnemies totalEnemies;

    private int totalSpawns = 0;
    private List<GameObject> enemies = new List<GameObject>();
    private float timeNextSpawn;
    private Vector3 positionNextSpawn;
    private Quaternion rotationNextSpawn;
    private bool playerInRange = false;

    private void Start()
    {
        managerObject = GameObject.Find("GameManager");
        totalEnemies = managerObject.GetComponent<TotalEnemies>();
        centerOfBox = transform.position;
        startSpawnDelay += Time.time;
        playerInRange = startSpawnOnStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startSpawnDelay)
        {
            SpawnEnemiesWithDelay();
        }
    }

    private void SpawnEnemiesWithDelay()
    {
        if (
            Time.time >= timeNextSpawn
            && transform.childCount < countMaxEnemies
            && totalSpawns <= totalMaxSpawns
            && totalEnemies.CanSpawnEnemy()
            && playerInRange
        )
        {
            totalEnemies.SpawnEnemy();
            timeNextSpawn = Time.time + delaySpawn;
            SpawnEnemyRandomPosition(prefabEnemy);
            totalSpawns++;
            RandomizePosistionNextSpawn();
        }
    }

    private Vector3 RandomizePosistionNextSpawn()
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        newPosition.x = Random.Range(centerOfBox.x - boxWidth, centerOfBox.x + boxWidth);
        newPosition.z = Random.Range(centerOfBox.z - boxWidth, centerOfBox.z + boxLength);
        newPosition.y = Random.Range(centerOfBox.y - boxWidth, centerOfBox.y + boxHeight);
        return newPosition;
    }

    private void SpawnEnemyRandomPosition(GameObject enemy)
    {
        Vector3 randomPosition = RandomizePosistionNextSpawn();
        GameObject newEnemy = Instantiate(enemy, randomPosition, rotationNextSpawn);
        enemies.Add(newEnemy);
        newEnemy.transform.parent = gameObject.transform;
        newEnemy.transform.position = randomPosition;
    }

    public void PlayerInRange()
    {
        playerInRange = true;
    }
}
