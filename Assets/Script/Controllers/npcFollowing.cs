using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcFollowing : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    public LayerMask whatIsPlayer;
    public float sightRange;
    public bool playerInSightRange;
    Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player =  GameObject.Find("Player").transform;
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
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
    }
    public void playerFollow()
    {
        agent.speed = 2;
        if (target.rotation.y >= 90 && target.rotation.y <= 180){
            agent.destination = new Vector3(target.position.x-2, target.position.y, target.position.z-2);
        }//第二象限
        else if (target.rotation.y < 0 && target.rotation.y >= -90){
            agent.destination = new Vector3(target.position.x-2, target.position.y, target.position.z-2);
        }//第四象限
        else if (target.rotation.y < -90 && target.rotation.y >= -180){
            agent.destination = new Vector3(target.position.x-2, target.position.y, target.position.z+2);
        }//第一象限
        else if(target.rotation.y < 90 && target.rotation.y > 0){
            agent.destination = new Vector3(target.position.x+2, target.position.y, target.position.z-2);
        }//第三象限
    }
}

