using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Image reloadBar;
    [SerializeField] private Image barFillImage;
    [SerializeField] private Text ammoText;

    public float nextAttackTimer = 0f;
    //public float attackFinishedTimer = 0f;
    //public float attackFinishedResetTimer = 0f;
    private bool isAttacking = false;
   // private List<Target> targetsHit;

    void Start()
    {
        //targetsHit= new List<Target>();
    }

    void OnEnable()
    {
        reloadBar.gameObject.SetActive(false);
        barFillImage.gameObject.SetActive(false);
        ammoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target != null && isAttacking == true)// && target.isHitByKnife == false)
        {
            Debug.Log("Target hit with knife");
            target.TakeDamage(damage);
            
            /*if(targetsHit.Contains(target)) 
            {
                int targetHitIndex = targetsHit.IndexOf(target);
                if (targetsHit[targetHitIndex].isHitByKnife == true)
                {
                    return;
                }
                else
                {
                    Debug.Log("Target hit with knife and inside array");
                    Debug.Log("Target hit is: " + targetsHit[targetHitIndex].isHitByKnife);
                    targetsHit[targetHitIndex].TakeDamage(damage);
                    targetsHit[targetHitIndex].isHitByKnife = true;
                    StartCoroutine(ResetKnifeHitOnTarget(targetsHit, targetHitIndex, attackFinishedTimer));
                }
            }
            else
            {
                Debug.Log("Target hit with knife and outside array");
                targetsHit.Add(target);
                targetsHit[targetsHit.Count - 1].TakeDamage(damage);
                //target.isHitByKnife = true;
                targetsHit[targetsHit.Count - 1].isHitByKnife = true; 
                StartCoroutine(ResetKnifeHitOnTarget(targetsHit, targetsHit.Count - 1, attackFinishedTimer));
            }*/
        }
    }

    void Update()
    {
        float attackDuration = 2.033f;

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTimer)
        {
            StartCoroutine(AttackCoroutine(attackDuration));
            
            /*Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange);
            Debug.Log("Enemies hit: " + hitEnemies.Length);*/
        }
    }

    /*IEnumerator ResetKnifeHitOnTarget(List<Target> targetsHit, int targetHitIndex, float attackFinishedTimer)
    {
        if(targetsHit[targetHitIndex].isHitByKnife == true)
        {      
            //attackFinishedResetTimer = attackFinishedTimer;
            if(Time.time >= attackFinishedTimer)
            {
                Debug.Log("Called and used if");
                targetsHit[targetHitIndex].isHitByKnife = false;
            }
            else
            {
                Debug.Log("Called and used else");
                yield return new WaitForSeconds(attackFinishedTimer - Time.time);
                targetsHit[targetHitIndex].isHitByKnife = false;
            }
        }

*//*        if(target.isHitByKnife == true) 
        {
            if(Time.time >= nextAttackTimer)
            {
                target.isHitByKnife = false;
            }
            else
            {
                yield return null;
            }

            //targetsHit.Remove(target);
        }*//*
    }*/

    IEnumerator AttackCoroutine(float attackDuration)
    {
        nextAttackTimer = Time.time + attackDuration;

        isAttacking = true;
        animator.SetLayerWeight(3, 1f);
        animator.SetBool("isAttacking", isAttacking);
        yield return new WaitForSeconds(attackDuration);

        //attackFinishedTimer = Time.time;
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetLayerWeight(3, 0f);
    }
}
