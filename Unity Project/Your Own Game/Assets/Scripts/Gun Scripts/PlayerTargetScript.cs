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

    [SerializeField]
    private GameObject flashObject;

    private void Awake() 
    {
        flashObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(ShowAndHideFlash(flashObject, 1f));
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

    public void GainHealth(float healthToGain)
    {
        health += healthToGain;
    }

    IEnumerator ShowAndHideFlash(GameObject screenFlash, float delay)
    {
        screenFlash.SetActive(true);
        yield return new WaitForSeconds(delay);
        screenFlash.SetActive(false);
    }
}
