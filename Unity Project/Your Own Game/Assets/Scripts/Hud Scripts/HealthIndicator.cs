using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player; 

    private float health = 0;
    Target target;
    Text healthText;
    void Start()
    {
        player = GameObject.Find("Player");
        target = player.GetComponent<Target>();
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        health = target.GetHealth();
        healthText.text = health.ToString();
    }
}
