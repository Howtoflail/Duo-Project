
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWithAnimation : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private float deathAnimationDuration = 3f;

    private float animationTime;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
    }

    private void Update()
    {    
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            
            animator.SetTrigger("Die");
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

    void DieWithAnimation(float delay)
    {
        
    }

    public float GetHealth()
    {
        return health;
    }
}

