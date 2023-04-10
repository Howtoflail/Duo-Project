using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject projectile;

    //
    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;
    
    //
    [Header("Throwing")]
    public float throwForce;
    public float throwForceUp;
    private bool readyToThrow;
    void Start()
    {
        readyToThrow = true;
    }

    public void Throw()
    {
        Debug.Log("THROW");
        readyToThrow = false;

        GameObject newProjectile = Instantiate(projectile, attackPoint.position, cam.rotation);

        Rigidbody projectileRB = newProjectile.GetComponent<Rigidbody>();

        // //adjustment
        // Vector3 forceDirection = camera

        Vector3 projectileForce = cam.transform.forward * throwForce + transform.up * throwForceUp;

        projectileRB.AddForce(projectileForce, ForceMode.Impulse);  

        totalThrows--;;

        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P) && readyToThrow && totalThrows > 0)
        // {
        //     Throw();
        // }
    }
}
