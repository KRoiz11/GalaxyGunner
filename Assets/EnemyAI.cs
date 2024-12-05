using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{   
    //references
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator enemyAnimator;
    public BoxCollider boxCollider;

    //For Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //For Attacking
    public float timeBetweenAttacks;
    public float damage;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }
 
    //Update is called once per frame
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Conditions for each state
        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            enemy.SetDestination(walkPoint);

        //calculates distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walk point reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {   
        //finds random number within range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //calculates new walkpoint
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {   //makes enemy chase player by its position
        enemy.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if(!alreadyAttacked)
        {
            if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Z_Attack"))
            {
                //triggers the attack animation
                enemyAnimator.SetTrigger("Attack");
                //make sure enemy doesn't move
                enemy.SetDestination(transform.position);
                //make sure that the enemy looks at the player
                transform.LookAt(player);
            }
            
            alreadyAttacked = true;
            //causes enemy to attack after specified time
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    //resets the enemy attack
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //turns the box collider on
    private void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    //turns the box collider off
    private void DisableAttack()
    {
        boxCollider.enabled = false;
    }

}


