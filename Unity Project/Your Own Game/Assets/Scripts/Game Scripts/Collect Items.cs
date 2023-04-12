using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    [SerializeField]
    private float medkitHealth;
    [SerializeField]
    private float shotGunAmmo;
    [SerializeField]
    private float rifleAmmo;

    // Start is called before the first frame update
    [SerializeField]
    private PlayerTarget playerTarget;
    [SerializeField]
    private MainGun mainGun;
    [SerializeField]
    private SecondaryGun secondaryGun;

    [SerializeField]
    private float pickUpDistance;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private LayerMask whatIsCollectable;

    void Start()
    {
        playerTarget = GameObject.Find("Player").GetComponent<PlayerTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (
                Physics.Raycast(
                    cameraTransform.position,
                    cameraTransform.forward,
                    out hit
                )
            )
            {
                if (1 << hit.transform.gameObject.layer == whatIsCollectable.value)
                {
                    string collectableName = hit.transform.gameObject.GetComponent<Collectable>().GetCollectableName();
                    switch (collectableName)
                    {
                        case "Medkit":
                            playerTarget.GainHealth(medkitHealth);
                            break;
                        case "ShotGunAmmo":
                            // playerTarget.GainHealth(medkitHealth);
                            Debug.Log("ShotGun Ammo Grabbed");
                            secondaryGun.AddAmmo(shotGunAmmo);
                            break;
                        case "RifleAmmo":
                            // playerTarget.GainHealth(medkitHealth);
                            mainGun.AddAmmo(rifleAmmo);
                            Debug.Log("Rifle Ammo Grabbed");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
