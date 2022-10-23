using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob_UmbrellaController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    Transform target;
    public GameObject[] items;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange = 5;

    //Attacking
    public Vector3 attackPoint;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private int randomIndex;

    public GameObject generate;
    private GenerateEnemy int_enemyCount;
    float height;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animation
    public Animator anim;

    private void Awake()
    {
        // player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;
        int_enemyCount = generate.GetComponent<GenerateEnemy>();
        randomIndex = Random.Range(0, items.Length);
        // TakeDamage(10);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }


        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            walkPointSet = false;
            StartCoroutine(waiter());
            anim.SetBool("isWalking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            anim.SetBool("isWalking", false);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(100);
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = 2;
        agent.destination = target.position;
        // Invoke(3);
    }

    private void AttackPlayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        FaceTarget();

        if (!alreadyAttacked)
        {
            anim.SetBool("isAttacking", true);
            //Attack Code
            height = 0.3f;
            attackPoint = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            Rigidbody rb = Instantiate(projectile, attackPoint, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 35f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            // var bullet = Instantiate(projectile, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        if(alreadyAttacked)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Vector3 position = transform.position;
        if (health <= 0)
        {
            Instantiate(
                items[randomIndex], 
                position + new Vector3(0.0f, 15, 0.0f), 
                Quaternion.identity);

            Invoke(nameof(DestroyEnemy), 0.5f);

        }
    }

    public void DestroyEnemy()
    {
        int_enemyCount.enemyCount -= 1;
        Destroy(gameObject);
    }
}
