using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public LayerMask whatIsPlayer;
    public float sightRange;
    public bool playerInSightRange;
    Transform target;
    public Animator anim;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject nowFollowing;


    void Start()
    {
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Following Player
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (!playerInSightRange)
        {
            playerFollow();
            nowFollowing.anim.SetBool("isWalking", true);
        }
        else if (playerInSightRange)
        {
            nowFollowing.anim.SetBool("isWalking", false);
        }
    }
    public void playerFollow()
    {
        agent.speed = 2;
        agent.destination = target.position;
    }
}
