using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] npc;
    public GameObject gameManager;
    public Transform player;
    private int i=0;
    public bool[] choosed = new bool[7];
    Transform following;

    private GameObject godManager;
    private attendantManager aM;


    void Start()
    {
        following = this.transform;
        player =  GameObject.Find("Player").transform;
        aM = gameManager.GetComponent<attendantManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startFollowing(){
        godManager = Instantiate (npc[i], new Vector3(player.position.x-5,player.position.y, player.position.z-5) , Quaternion.identity);
        godManager.transform.parent = gameObject.transform;
    }

    public void nowFollowing(){
        if(aM.rabbitArea.activeSelf && choosed[0]){
            i=0;
        }
        else if(aM.wolfArea.activeSelf && choosed[1]){
            i=1;       
        }
        
        else if(aM.foxArea.activeSelf && !choosed[2]){
            i=2;
        }
        else if(!choosed[0] || !choosed[1] || !choosed[2]){
            Destroy(following.GetChild(0).gameObject);
        }
    }

}
