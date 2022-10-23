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
    private Animator anim;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject nowFollowing;
    private GameObject gameManager;
    private attendantManager aM;
    public GameObject[] npcs = new GameObject[7];
    private npcRabbit nR;


    void Start()
    {
        target = player.transform;
        aM = gameManager.GetComponent<attendantManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(aM.rabbitArea.activeSelf){
            nowFollowing = npcs[0];
            playerFollow();
        }
        //Following Player
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }
    public void playerFollow()
    {
        if (!playerInSightRange)
        {
            //nowFollowing.anim.SetBool("isWalking", true);
            agent.speed = 2;
            agent.destination = target.position;
        }
        else if (playerInSightRange)
        {
           // nowFollowing.anim.SetBool("isWalking", false);
        }
    }
}
