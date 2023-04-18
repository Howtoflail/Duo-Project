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
    [SerializeField] GameObject secondHandGrabShot;
    [SerializeField] GameObject leftHandHintShot;
    [SerializeField] GameObject rightHandHintShot;
    [SerializeField] GameObject leftHandHintRifle;
    [SerializeField] GameObject rightHandHintRifle;
    [SerializeField] GameObject rightHandHintKnife;
    [SerializeField] GameObject leftHandTargetKar;
    [SerializeField] GameObject rightHandTargetKar;
    [SerializeField] GameObject leftHandTargetShot;
    [SerializeField] GameObject rightHandTargetShot;
    [SerializeField] GameObject rightHandTargetKnife;
    [SerializeField] GameObject leftArm;
    [SerializeField] GameObject leftForeArm;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject rightForeArm;
    [SerializeField] GameObject rightHand;

    GameObject player;
    RigBuilder rigBuilder;
    Animator animator;
    MainGun rifleReference;
    SecondaryGun shotgunReference;
    TwoBoneIKConstraint leftHandTwoBoneIKShot;
    TwoBoneIKConstraint rightHandTwoBoneIKShot;
    TwoBoneIKConstraint secondHandGrabTwoBoneIK;

    void Start()
    {
        player = GameObject.Find("Player");
        rigBuilder = player.GetComponent<RigBuilder>();
        animator = player.GetComponent<Animator>();
        rifleReference = rifle.GetComponent<MainGun>();
        shotgunReference = shotgun.GetComponent<SecondaryGun>();
        leftHandTwoBoneIKShot = leftHandIKShot.GetComponent<TwoBoneIKConstraint>();
        rightHandTwoBoneIKShot = rightHandIKShot.GetComponent<TwoBoneIKConstraint>();
        secondHandGrabTwoBoneIK = secondHandGrabShot.GetComponent<TwoBoneIKConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && rifle.activeSelf == false)
        {
            //This will prevent changing weapons if the sound (animation) of the gun shot hasnt finished playing
            if (Time.time < shotgunReference.nextTimeToFire || animator.GetBool("isAttacking") == true)
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

            //Reseting all constraints and attaching them again
            leftHandTwoBoneIKShot.Reset();
            leftHandTwoBoneIKShot.data.root = leftArm.transform;
            leftHandTwoBoneIKShot.data.mid = leftForeArm.transform;
            leftHandTwoBoneIKShot.data.tip = leftHand.transform;
            leftHandTwoBoneIKShot.data.target = leftHandTargetKar.transform;
            leftHandTwoBoneIKShot.data.hint = leftHandHintRifle.transform;
            rightHandTwoBoneIKShot.Reset();
            rightHandTwoBoneIKShot.data.root = rightArm.transform;
            rightHandTwoBoneIKShot.data.mid = rightForeArm.transform;
            rightHandTwoBoneIKShot.data.tip = rightHand.transform;
            rightHandTwoBoneIKShot.data.target = rightHandTargetKar.transform;
            rightHandTwoBoneIKShot.data.hint = rightHandHintRifle.transform;
            secondHandGrabTwoBoneIK.Reset();
            secondHandGrabTwoBoneIK.data.root = leftArm.transform;
            secondHandGrabTwoBoneIK.data.mid = leftForeArm.transform;
            secondHandGrabTwoBoneIK.data.tip = leftHand.transform;
            secondHandGrabTwoBoneIK.data.target = leftHandTargetKar.transform;
            secondHandGrabTwoBoneIK.data.hint = leftHandHintRifle.transform;

            rigBuilder.Build();
            animator.Rebind();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && shotgun.activeSelf == false)
        {
            if (Time.time < rifleReference.nextTimeToFire || animator.GetBool("isAttacking") == true)
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

            //Reseting all constraints and attaching them again
            leftHandTwoBoneIKShot.Reset();
            leftHandTwoBoneIKShot.data.root = leftArm.transform;
            leftHandTwoBoneIKShot.data.mid = leftForeArm.transform;
            leftHandTwoBoneIKShot.data.tip = leftHand.transform;
            leftHandTwoBoneIKShot.data.target = leftHandTargetShot.transform;
            leftHandTwoBoneIKShot.data.hint = leftHandHintShot.transform;
            rightHandTwoBoneIKShot.Reset();
            rightHandTwoBoneIKShot.data.root = rightArm.transform;
            rightHandTwoBoneIKShot.data.mid = rightForeArm.transform;
            rightHandTwoBoneIKShot.data.tip = rightHand.transform;
            rightHandTwoBoneIKShot.data.target = rightHandTargetShot.transform;
            rightHandTwoBoneIKShot.data.hint = rightHandHintShot.transform;
            secondHandGrabTwoBoneIK.Reset();
            secondHandGrabTwoBoneIK.data.root = leftArm.transform;
            secondHandGrabTwoBoneIK.data.mid = leftForeArm.transform;
            secondHandGrabTwoBoneIK.data.tip = leftHand.transform;
            secondHandGrabTwoBoneIK.data.target = leftHandTargetShot.transform;
            secondHandGrabTwoBoneIK.data.hint = leftHandHintShot.transform;

            rigBuilder.Build();
            animator.Rebind();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && knife.activeSelf == false)
        {
            if (Time.time < rifleReference.nextTimeToFire || Time.time < shotgunReference.nextTimeToFire)
            {
                return;
            }

            if (rifleReference.isReloading == true)
            {
                rifleReference.isReloading = false;
            }
            if (shotgunReference.isReloading == true)
            {
                shotgunReference.isReloading = false;
            }

            rifle.SetActive(false);
            shotgun.SetActive(false);
            knife.SetActive(true);

            //Reseting all constraints and attaching them again
            //No left hand needed since knife is in right hand
            leftHandTwoBoneIKShot.Reset();
            rightHandTwoBoneIKShot.Reset();
            rightHandTwoBoneIKShot.data.root = rightArm.transform;
            rightHandTwoBoneIKShot.data.mid = rightForeArm.transform;
            rightHandTwoBoneIKShot.data.tip = rightHand.transform;
            rightHandTwoBoneIKShot.data.target = rightHandTargetKnife.transform;
            rightHandTwoBoneIKShot.data.hint = rightHandHintKnife.transform;
            secondHandGrabTwoBoneIK.Reset();

            rigBuilder.Build();
            animator.Rebind();
        }
    }
}
