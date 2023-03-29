using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LesserMovement : EnemyMovement
{
    //Animation
    private float currentSpeed;
    private Vector3 previousPosition;
    private Animator animator;

    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround,
        whatIsPlayer;

    //State 1 Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //State 2 Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States general
    public float sightRange,
        attackRange;
    public bool playerInSightRange,
        playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curMove = transform.position - previousPosition;
        currentSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        animator.SetFloat("Speed", currentSpeed);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            ChasingPlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackingPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
            LookForWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasingPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackingPlayer()
    {
        agent.SetDestination(player.position);

        // transform.LookAt(player);
        Vector3 rot = Quaternion.LookRotation(player.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);

        if (!alreadyAttacked)
        {
            var target = player.GetComponent<Target>();
            target.TakeDamage(10f);
            animator.SetBool("IsAttacking", true);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("IsAttacking", false);
    }

    private void LookForWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
