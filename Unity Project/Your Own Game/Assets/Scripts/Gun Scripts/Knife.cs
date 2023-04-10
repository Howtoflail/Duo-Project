using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;

    public float nextTimeToStrike = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange);
            Debug.Log("Enemies hit: " + hitEnemies.Length);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
