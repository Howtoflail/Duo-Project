using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;

    [SerializeField]
    private float deathAnimationDuration = 3f;
    private Animator animator;

    private EnemyMovement enemyMovement;
    private GameObject managerObject;
    TotalEnemies totalEnemies;

    private bool isDead = false;

    private void Awake()
    {
        managerObject = GameObject.Find("GameManager");
        totalEnemies = managerObject.GetComponent<TotalEnemies>();
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f && !isDead)
        {
            animator.SetTrigger("Die");
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Debug.Log("DEAD");
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, deathAnimationDuration);
        isDead = true;
        totalEnemies.RemoveEnemy();
    }

    public float GetHealth()
    {
        return health;
    }
}
