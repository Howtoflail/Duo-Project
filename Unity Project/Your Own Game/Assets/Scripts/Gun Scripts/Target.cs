    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;
    [SerializeField] private float deathAnimationDuration = 3f;
    private Animator animator;

    private BasicEnemyMovement enemyMovement;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<BasicEnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            animator.SetTrigger("Die");
            enemyMovement.enabled = false;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, deathAnimationDuration);
    }

    public float GetHealth()
    {
        return health;
    }
}
