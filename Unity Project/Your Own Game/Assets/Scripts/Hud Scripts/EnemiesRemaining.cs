using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesRemaining : MonoBehaviour
{
    // Start is called before the first frame update\
    GameObject managerObject; 

    private int enemiesRemaining;
    TotalEnemies totalEnemies;
    Text enemiesText;
    void Start()
    {
        managerObject = GameObject.Find("GameManager");
        totalEnemies = managerObject.GetComponent<TotalEnemies>();
        enemiesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesRemaining = totalEnemies.GetEnemiesRemaining();
        enemiesText.text = "Enemies Remaining : " + enemiesRemaining;
    }
}
