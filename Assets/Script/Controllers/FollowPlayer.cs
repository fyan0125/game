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
        player = GameObject.Find("Player").transform;
        gameManager = GameObject.Find("GameManager");
        aM = gameManager.GetComponent<attendantManager>();
    }

    void Awake() 
    {
        this.transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startFollowing(){
        godManager = Instantiate (npc[i], new Vector3(player.position.x-2,player.position.y, player.position.z-2) , Quaternion.identity);
        godManager.transform.parent = gameObject.transform;
        Debug.Log("Following");
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
        else if(aM.chickenArea.activeSelf && choosed[3]){
            i=3;       
        }
        else if(aM.craneArea.activeSelf && !choosed[4]){
            i=4;
        }
        else if(!choosed[0] || !choosed[1] || !choosed[2] || !choosed[3] || !choosed[4]){
            Destroy(following.GetChild(0).gameObject);
        }
    }

}
