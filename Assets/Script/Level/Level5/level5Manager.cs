using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class level5Manager : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject Crane;
    public npcCrane npcCrane;
    public float time = 90;

    [Header("遊戲進行中")]

    private ThirdPersonChar player;
    private craneObject craneObject;

    public Animator craneAnim;

    public GameObject assetToHide;
    public Transform targetObject;
    public LayerMask npcLayer;
    public float sightRange;
    public bool targetObjectInSightRange;
    int childCount=0;

    private void Start()
    {
        SwitchSkills.getSkill = 2;
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        Crane = GameObject.Find("NPC");
        npcCrane = Crane.GetComponent<npcCrane>();
        craneAnim = Crane.transform.GetChild(1).gameObject.GetComponent<Animator>();

        //變身物件
        turnIntoObject();
        Timer.time = time;
    }

    private void Update()
    {
        if(targetObject != null){
            targetObjectInSightRange = Physics.CheckSphere(targetObject.position, sightRange, npcLayer);
            if(targetObjectInSightRange){
                Crane.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void GameStart()
    {
        Resume();
        GameUI.SetActive(true);
        if(targetObject != null){
            runToTarget();
        }
    }
    
    public void turnIntoObject(){
        assetToHide = GameObject.Find("Assets");
        childCount = assetToHide.transform.childCount;
        targetObject = assetToHide.transform.GetChild(Random.Range(0, childCount)).gameObject.transform;
        targetObject.transform.parent = gameObject.transform;
        targetObject.gameObject.AddComponent<craneObject>();
        craneObject = targetObject.GetComponent<craneObject>();
        craneAnim.SetBool("isWalking", false);
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void GameComplete()
    {
        Debug.Log("Level5 Complete");
        Timer.setTimeToPause();
        GameUI.SetActive(false);
        Crane.transform.position = new Vector3(20, 3, -10);
        npcCrane.agent.destination = new Vector3(20, 3, -10);
        Crane.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        Crane.transform.GetChild(1).gameObject.SetActive(true);
        SwitchSkills.getSkill = 3;
        npcCrane.gameComplete = true;
    }

    public void runToTarget()
    {
        npcCrane.agent.speed = 7;
        npcCrane.agent.destination = targetObject.position;
        craneAnim.SetBool("isWalking", true);
    }
}
