using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodManagerFollowing : MonoBehaviour
{
    public GameObject player;
    public Animator[] anim = new Animator[5];
    private NewThirdPersonChar thirdPersonChar;
    public int i;
    public UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        for(int j=0 ; j<6 ; j++)
        {
            anim[j] = this.transform.GetChild(j).gameObject.GetComponent<Animator>();
        }
        //anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        thirdPersonChar = player.GetComponent<NewThirdPersonChar>();
    }

    void Update()
    {
        if( thirdPersonChar.PlayerIsWalking )
        {
            for(int j= 0 ; j<6 ; j++)
            {
                anim[j].SetBool("isWalking", true);
            }
            
            playerFollow();
        }
        else
        {
            for(int j=0 ; j<6 ; j++)
            {
                anim[j].SetBool("isWalking", false);
            }
            
        }
    }

    public void playerFollow()
    {
        agent.speed = 4;
        if (player.transform.rotation.y >= 90 && player.transform.rotation.y <= 180){
            agent.destination = new Vector3(player.transform.position.x-2, player.transform.position.y, player.transform.position.z-2);
        }//第二象限
        else if (player.transform.rotation.y < 0 && player.transform.rotation.y >= -90){
            agent.destination = new Vector3(player.transform.position.x-2, player.transform.position.y, player.transform.position.z-2);
        }//第四象限
        else if (player.transform.rotation.y < -90 && player.transform.rotation.y >= -180){
            agent.destination = new Vector3(player.transform.position.x-2, player.transform.position.y, player.transform.position.z+2);
        }//第一象限
        else if(player.transform.rotation.y < 90 && player.transform.rotation.y > 0){
            agent.destination = new Vector3(player.transform.position.x+2, player.transform.position.y, player.transform.position.z-2);
        }//第三象限
    }
}
