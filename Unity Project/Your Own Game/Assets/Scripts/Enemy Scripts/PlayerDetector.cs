using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private EnemySpawner enemySpawner;
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other) {

        if (other.transform.tag == "Player")
        {
            enemySpawner.PlayerInRange();
            Debug.Log("Player detecte3d");
        }
    }
    }

  

