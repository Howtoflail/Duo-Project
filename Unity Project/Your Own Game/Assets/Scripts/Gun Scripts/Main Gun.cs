using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MainGun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 2.2f;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] private AudioClip rifleShotClip;
    [SerializeField] private AudioClip rifleCockClip;
    [SerializeField] private AudioClip rifleReloadClip;

    public Camera fpsCam;
    private AudioSource audioSource;

    private float nextTimeToFire = 0f;
    private readonly float defaultMagazineSize = 5f;
    private float currentMagazineAmmo = 0f;
    private bool isReloading = false;
    private bool isCocking = false;
    private float shotTime = 0f;
    private float waitTime = 3f;

    //Input - later
    //private bool fireButton = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentMagazineAmmo = defaultMagazineSize;
    }

    void Update()
    {
        //GetKeyDown is used because of the bolt action system
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire && currentMagazineAmmo > 0)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();

            //play audio
            float currentAudioClipDuration = rifleShotClip.length;
            audioSource.clip = rifleShotClip;
            audioSource.Play();
            Debug.Log(audioSource.clip.name);

            //Cocking coroutine is not needed
            /*if (audioSource.isPlaying == false && audioSource.clip == rifleCockClip && currentMagazineAmmo > 1f) 
            {
                Debug.Log("Current ammo: " + currentMagazineAmmo);
                Cock();
            } */ 
        }
        else if(currentMagazineAmmo <= 0) 
        {
            //This is used to make the reload sound play after the last round is fired properly
            if(Time.time - shotTime >= waitTime) 
            {
                Reload();
            }
        }
    }

    /*void Cock()
    {
        if(isCocking == false)
        {
            isCocking = true;
            StartCoroutine(CockingCoroutine());
        }
    }

    IEnumerator CockingCoroutine()
    {
        float cockingTime = rifleCockClip.length;

        audioSource.PlayOneShot(rifleCockClip);
        yield return new WaitForSeconds(cockingTime);
        isCocking = false;
    }*/

    void Reload()
    {
        if(isReloading == false)
        {
            isReloading = true;
            StartCoroutine(ReloadCoroutine());

            //Cock();
        }
    }

    IEnumerator ReloadCoroutine()
    {
        float reloadTime = rifleReloadClip.length;

        audioSource.PlayOneShot(rifleReloadClip);
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        currentMagazineAmmo = defaultMagazineSize;
    }

    void Shoot()
    {
        currentMagazineAmmo--;
        shotTime = Time.time;

        RaycastHit hit;
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green, 10, false);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);


            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

            }

            //Could lead to issues with AI movement
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
