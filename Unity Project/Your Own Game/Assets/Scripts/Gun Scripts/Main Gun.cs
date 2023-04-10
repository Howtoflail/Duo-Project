using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Light muzzleLight;
    [SerializeField] private GameObject impact;

    private Text ammoText;
    private GameObject ammoTextObject;
    public Camera fpsCam;
    private AudioSource audioSource;

    public float nextTimeToFire = 0f;
    private readonly float defaultMagazineSize = 5f;
    private float currentMagazineAmmo = 0f;
    private float allAmmo = 20f;
    public bool isReloading = false;
    private bool isCocking = false;

    private float shotTime = 0f;
    private float waitTime = 3f;

    //Input - later
    //private bool fireButton = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentMagazineAmmo = defaultMagazineSize;
        ammoTextObject = GameObject.Find("Canvas/Ammo");
        ammoText = ammoTextObject.GetComponent<Text>();
    }

    void Update()
    {
        //Displaying ammo
        //Fix constantly updating this text
        ammoText.text = currentMagazineAmmo.ToString() + "/" + allAmmo.ToString();

        //Stopping the muzzle light from being on at all times
        if (muzzleFlash.isPlaying == true)
        {
            muzzleLight.enabled = true;
        }
        else
        {
            muzzleLight.enabled = false;
        }

        //GetKeyDown is used because of the bolt action system
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire && currentMagazineAmmo > 0f && isReloading == false)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
            
            //play audio
            float currentAudioClipDuration = rifleShotClip.length;
            audioSource.clip = rifleShotClip;
            audioSource.Play();
            Debug.Log(audioSource.clip.name);
        }
        //if magazine is empty proceed to reload
        else if(currentMagazineAmmo <= 0f && allAmmo > 0f) 
        {
                Reload();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentMagazineAmmo < 5 && allAmmo > 0f)
        {
            Reload();
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
        float cockingTime = shotgunCockClip.length;

        audioSource.PlayOneShot(shotgunCockClip);
        yield return new WaitForSeconds(cockingTime);
        isCocking = false;
    }*/

    void Reload()
    {
        if(isReloading == false)
        {
            isReloading = true;

            float remainingTime = waitTime - (Time.time - shotTime);
            Debug.Log("Remaining time before reloading: " + remainingTime);
            if(remainingTime > 0f)
            {
                StartCoroutine(WaitAndReloadCoroutine(remainingTime));
            }
            else
            {
                //Start the reload coroutine immediately if the gunshot clip is not playing or has finished playing
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    //This coroutine is placed in order to register the need to reload even when the gunshot clip/animation is not done playing
    IEnumerator WaitAndReloadCoroutine(float remainingTime) 
    {
        yield return new WaitForSeconds(remainingTime);
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        float reloadTime = rifleReloadClip.length;

        audioSource.PlayOneShot(rifleReloadClip);
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        if(allAmmo >= 5f)
        {
            allAmmo -= defaultMagazineSize - currentMagazineAmmo;
            currentMagazineAmmo = defaultMagazineSize;
        }
        else
        {
            currentMagazineAmmo = allAmmo;
            allAmmo = 0f;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        currentMagazineAmmo--;
        shotTime = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            GameObject effectInstance = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effectInstance, 1.5f);

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
