using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator[] npc = new Animator[7];
    public GameObject gameManager;
    public Transform player;
    public LayerMask whatIsPlayer;
    private int i;
    public float sightRange;
    public bool playerInSightRange;
    Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    private attendantManager aM;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = player.transform;
        aM = gameManager.GetComponent<attendantManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Following Player
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        nowFollowing();
        if (!playerInSightRange)
        {
            playerFollow();
            npc[i].SetBool("isWalking", true);
        }
        else if (playerInSightRange)
        {
            npc[i].SetBool("isWalking", false);
        }
    }
    public void nowFollowing(){
        if(aM.rabbitArea.activeSelf){
            i=0;
        }
        else if(aM.wolfArea.activeSelf){
            i=1;
        }
        else if(aM.foxArea.activeSelf){
            i=2;
        }
    }
    public void playerFollow()
    {
        agent.speed = 2;
        agent.destination = target.position;
    }
}
