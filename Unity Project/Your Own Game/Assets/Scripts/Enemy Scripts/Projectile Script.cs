using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float projectileDamage;
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.ToString());
        if (other.collider.tag == "Player")
        {
            var target = other.gameObject.GetComponent<Target>();
            target.TakeDamage(projectileDamage);
                    Destroy(gameObject);

        }
    }
}
