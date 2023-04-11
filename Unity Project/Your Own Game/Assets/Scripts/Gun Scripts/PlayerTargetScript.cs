using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerTarget : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;
    private bool isDead = false;

    private void Awake() { }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f && !isDead)
        {
            SceneManager.LoadScene("Death Screen");

            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Debug.Log("DEAD");
            Die();
        }
    }

    void Die() { }

    public float GetHealth()
    {
        return health;
    }
}
