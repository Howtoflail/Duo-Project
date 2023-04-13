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
            //Disable all constraints for shotgun AND KNIFE
            /*leftHandIKShot.SetActive(false);
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
            rightHandHintRifle.SetActive(true);*/

            //Trying to properly switch the rigging
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
            //Disable all constraints for rifle AND KNIFE
            /*leftHandIKRifle.SetActive(false);
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
            rightHandHintShot.SetActive(true);*/

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

            //disable shotgun and rifle iks and enable knife ik
            //disable second hand grab and no left hand ik needed
            /*leftHandIKRifle.SetActive(false);
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
            rightHandHintKnife.SetActive(true);*/

            leftHandTwoBoneIKShot.Reset();
            /*            leftHandTwoBoneIKShot.data.root = leftArm.transform;
                        leftHandTwoBoneIKShot.data.mid = leftForeArm.transform;
                        leftHandTwoBoneIKShot.data.tip = leftHand.transform;
                        leftHandTwoBoneIKShot.data.target =*/
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
