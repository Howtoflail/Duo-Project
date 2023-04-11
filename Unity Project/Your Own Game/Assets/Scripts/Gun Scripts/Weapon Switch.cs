using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject leftHandIKShot;
    [SerializeField] GameObject rightHandIKShot;
    [SerializeField] GameObject leftHandIKRifle;
    [SerializeField] GameObject rightHandIKRifle;
    [SerializeField] GameObject rightHandIKKnife;
    [SerializeField] GameObject secondHandGrabShot;
    [SerializeField] GameObject secondHandGrabRifle;
    [SerializeField] GameObject leftHandHintShot;
    [SerializeField] GameObject rightHandHintShot;
    [SerializeField] GameObject leftHandHintRifle;
    [SerializeField] GameObject rightHandHintRifle;
    [SerializeField] GameObject rightHandHintKnife;

    GameObject player;
    RigBuilder rigBuilder;
    Animator animator;
    MainGun rifleReference;
    SecondaryGun shotgunReference;

    void Start()
    {
        player = GameObject.Find("Player");
        rigBuilder = player.GetComponent<RigBuilder>();
        animator = player.GetComponent<Animator>();
        rifleReference = rifle.GetComponent<MainGun>();
        shotgunReference = shotgun.GetComponent<SecondaryGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && rifle.activeSelf == false)
        {
            //This will prevent changing weapons if the sound (animation) of the gun shot hasnt finished playing
            if (Time.time < shotgunReference.nextTimeToFire)
            {
                return;
            }

            if(shotgunReference.isReloading == true)
            {
                shotgunReference.isReloading = false;
            }

            shotgun.SetActive(false);
            knife.SetActive(false);
            rifle.SetActive(true);
            //Disable all constraints for shotgun AND KNIFE
            leftHandIKShot.SetActive(false);
            rightHandIKShot.SetActive(false);
            secondHandGrabShot.SetActive(false);
            leftHandHintShot.SetActive(false);
            rightHandHintShot.SetActive(false);

            rightHandIKKnife.SetActive(false);
            rightHandHintKnife.SetActive(false);

            //Set contraints for rifle to active
            leftHandIKRifle.SetActive(true);
            rightHandIKRifle.SetActive(true);
            secondHandGrabRifle.SetActive(true);
            leftHandHintRifle.SetActive(true);
            rightHandHintRifle.SetActive(true);

            rigBuilder.Build();
            animator.Rebind();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && shotgun.activeSelf == false)
        {
            if (Time.time < rifleReference.nextTimeToFire)
            {
                return;
            }

            if (rifleReference.isReloading == true)
            {
                rifleReference.isReloading = false;
            }

            rifle.SetActive(false);
            knife.SetActive(false);
            shotgun.SetActive(true);
            //Disable all constraints for rifle AND KNIFE
            leftHandIKRifle.SetActive(false);
            rightHandIKRifle.SetActive(false);
            secondHandGrabRifle.SetActive(false);
            leftHandHintRifle.SetActive(false);
            rightHandHintRifle.SetActive(false);

            rightHandIKKnife.SetActive(false);
            rightHandHintKnife.SetActive(false);

            //Set contraints for shotgun to active
            leftHandIKShot.SetActive(true);
            rightHandIKShot.SetActive(true);
            secondHandGrabShot.SetActive(true);
            leftHandHintShot.SetActive(true);
            rightHandHintShot.SetActive(true);

            rigBuilder.Build();
            animator.Rebind();
        }
        /*if(Input.GetKeyDown(KeyCode.Alpha3) && knife.activeSelf == false)
        {
            //add check for knife here
            *//*if (Time.time < rifleReference.nextTimeToFire)
            {
                return;
            }*//*

            rifle.SetActive(false);
            shotgun.SetActive(false);
            knife.SetActive(true);

            //disable shotgun and rifle iks and enable knife ik
            //disable second hand grab and no left hand ik needed
            leftHandIKRifle.SetActive(false);
            rightHandIKRifle.SetActive(false);
            secondHandGrabRifle.SetActive(false);
            leftHandHintRifle.SetActive(false);
            rightHandHintRifle.SetActive(false);

            leftHandIKShot.SetActive(false);
            rightHandIKShot.SetActive(false);
            secondHandGrabShot.SetActive(false);
            leftHandHintShot.SetActive(false);
            rightHandHintShot.SetActive(false);
            
            //enable knife ik
            rightHandIKKnife.SetActive(true);
            rightHandHintKnife.SetActive(true);

            rigBuilder.Build();
            animator.Rebind();
        }*/
    }
}
