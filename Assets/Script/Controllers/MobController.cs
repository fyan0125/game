using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public static LayerMask whatIsPlayer;
    public float health;
    Transform target;
    public GameObject[] items = new GameObject[3];

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;

    //Attacking
    public Vector3 attackPoint;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private int randomIndex;

    public GameObject generate;

    // private GenerateEnemy int_enemyCount;
    float height;

    //States
    public float sightRange,
        attackRange;
    public bool playerInSightRange,
        playerInAttackRange;

    public Animator anim;
    public EnemyStats stats;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;
        stats = GetComponent<EnemyStats>();
        // int_enemyCount = generate.GetComponent<GenerateEnemy>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            anim.SetBool("isAttacking", false);
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            anim.SetBool("isAttacking", false);
        }
        if (playerInSightRange && playerInAttackRange)
        {
            BulletAttackPlayer();
            anim.SetBool("isAttacking", true);
        }
    }

    public void dropItem()
    {
        randomIndex = Random.Range(0, items.Length);

        Vector3 position = transform.position;
        GameObject item = GameObject.Instantiate(
            items[randomIndex],
            position + new Vector3(0.0f, 1, 0.0f),
            Quaternion.identity
        );
        // item.transform.Rotate (0f, 0f, 90f);
        // item.transform.rotation=Quaternion.identity;
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            StartCoroutine(waiter(500));
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            StartCoroutine(waiter(500));
            agent.SetDestination(walkPoint);
            walkPointSet = false;
            StartCoroutine(waiter(100));
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

    public IEnumerator waiter(int time)
    {
        yield return new WaitForSeconds(time);
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y + randomY,
            transform.position.z + randomZ
        );

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = 2;
        agent.destination = target.position;
        anim.SetBool("isWalking", true);
        // Invoke(3);
    }

    public void BulletAttackPlayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        FaceTarget();

        if (!alreadyAttacked)
        {
            //Attack Code
            height = 0.3f;
            attackPoint = new Vector3(
                transform.position.x,
                transform.position.y + height,
                transform.position.z
            );
            Rigidbody rb = Instantiate(projectile, attackPoint, Quaternion.identity)
                .GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 35f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            // var bullet = Instantiate(projectile, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        if (alreadyAttacked)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * 5f
        );
        anim.SetBool("isWalking", false);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public static void AttackPlayer(bool canAttack)
    {
        if (canAttack)
        {
            whatIsPlayer |= (1 << LayerMask.NameToLayer("Player"));
        }
        else
        {
            whatIsPlayer &= ~(1 << LayerMask.NameToLayer("Player"));
        }
    }
}
