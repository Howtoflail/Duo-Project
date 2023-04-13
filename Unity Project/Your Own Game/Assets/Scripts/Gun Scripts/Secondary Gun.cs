using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SecondaryGun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 15f;
    [SerializeField] private float fireRate = 2.2f;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] private AudioClip shotgunShotClip;
    [SerializeField] private AudioClip shotgunCockClip;
    [SerializeField] private AudioClip shotgunReloadClip;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Light muzzleLight;
    [SerializeField] private GameObject impact;
    [SerializeField] private Animator animator;
    [SerializeField] private Image reloadBar;
    [SerializeField] private Image barFillImage;

    [SerializeField] private Text ammoText;
    private GameObject ammoTextObject;
    public Camera fpsCam;
    private AudioSource audioSource;

    public float nextTimeToFire = 0f;
    private readonly float defaultMagazineSize = 2f;
    private float currentMagazineAmmo = 0f;
    private float allAmmo = 12f;
    public bool isReloading = false;
    private bool isCocking = false;

    private float shotTime = 0f;
    private float waitTime = 1.5f;
    private float reloadTime;

    //Input - later
    //private bool fireButton = false;
    void Start()
    {
        reloadTime = shotgunReloadClip.length - 0.5f;
        audioSource = GetComponent<AudioSource>();
        currentMagazineAmmo = defaultMagazineSize;
        ammoTextObject = GameObject.Find("Canvas/Ammo");
        //ammoText = ammoTextObject.GetComponent<Text>();
    }

    void OnEnable()
    {
        ammoText.gameObject.SetActive(true);
        reloadBar.gameObject.SetActive(false);
        barFillImage.gameObject.SetActive(false);
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
            float currentAudioClipDuration = shotgunShotClip.length;
            audioSource.clip = shotgunShotClip;
            audioSource.Play();
            Debug.Log(audioSource.clip.name);
        }
        //if magazine is empty proceed to reload
        else if (currentMagazineAmmo <= 0f && allAmmo > 0f)
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
        if (isReloading == false)
        {
            isReloading = true;
            /*animator.SetLayerWeight(1, 0f);
            animator.SetLayerWeight(2, 1f);
            animator.SetBool("isReloadingShotgun", true);*/

            float remainingTime = waitTime - (Time.time - shotTime);
            Debug.Log("Remaining time before reloading: " + remainingTime);
            if (remainingTime > 0f)
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

    IEnumerator FillReloadBarCoroutine()
    {
        reloadBar.gameObject.SetActive(true);
        barFillImage.gameObject.SetActive(true);

        float fillTime = 0f;
        float targetFillAmount = 1f;
        float fillSpeed = targetFillAmount / reloadTime;
        float fillAmount = 0f;
        while(fillTime < reloadTime)
        {
            fillAmount = fillSpeed * fillTime;
            barFillImage.fillAmount = fillAmount;
            yield return null;
            fillTime += Time.deltaTime;
            
        }
        fillAmount = 0f;
        barFillImage.fillAmount = fillAmount;

        reloadBar.gameObject.SetActive(false);
        barFillImage.gameObject.SetActive(false);
    }

    //This coroutine is placed in order to register the need to reload even when the gunshot clip/animation is not done playing
    IEnumerator WaitAndReloadCoroutine(float remainingTime)
    {
        yield return new WaitForSeconds(remainingTime);
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        audioSource.PlayOneShot(shotgunReloadClip);
        StartCoroutine(FillReloadBarCoroutine());
        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        /*animator.SetLayerWeight(1, 1f);
        animator.SetLayerWeight(2, 0f);
        animator.SetBool("isReloadingShotgun", false);*/

        if (allAmmo >= defaultMagazineSize)
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

        //Pallets of the shotgun
        ShotgunShot(out _, new Vector3(0.1f, 0f, 0f));
        ShotgunShot(out _, new Vector3(-0.1f, 0f, 0f));
        ShotgunShot(out _, new Vector3(0f, 0.1f, 0f));
        ShotgunShot(out _, new Vector3(0f, -0.1f, 0f));
    }

    void ShotgunShot(out RaycastHit hit, Vector3 addedPosition)
    {
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward + addedPosition, out hit, range))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward + addedPosition, Color.red, 15f);

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
    public void AddAmmo(float ammoToAdd)
    {
        allAmmo += ammoToAdd;
    }
}