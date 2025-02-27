using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using TMPro;

public class level5Manager : MonoBehaviour
{
    public GameObject GameUI, Crane, assetToHide;
    public npcCrane npcCrane;
    public float time = 90;

    [Header("遊戲進行中")]
    private ThirdPersonChar player;
    private craneObject craneObject;

    public Animator craneAnim;

    public Material newMaterialRef;

    public Transform[] targetObject = new Transform[3];
    public LayerMask npcLayer, craneLayer;
    public float sightRange;
    public bool targetObjectInSightRange;
    public int childCount, i = 0;
    public PostProcessProfile postProcessProfile;

    public Material[] mats;

    private void Start()
    {
        Timer.time = time;
        SwitchSkills.getSkill = 3;
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        Crane = GameObject.Find("NPC");
        GameUI = GameObject.Find("Level5UI").transform.GetChild(0).gameObject;
        npcCrane = Crane.GetComponent<npcCrane>();
        craneAnim = Crane.transform.GetChild(1).gameObject.GetComponent<Animator>();
        turnIntoObject();
    }

    private void Update()
    {
        if (targetObject[i] != null)
        {
            targetObjectInSightRange = Physics.CheckSphere(
                targetObject[i].position,
                sightRange,
                craneLayer
            );
            if (targetObjectInSightRange)
            {
                Crane.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else{

        }
    }

    public void GameStart()
    {
        //變身物件
        Resume();
        changeMaterial();
        GameUI.SetActive(true);
        Crane.transform.GetChild(1).gameObject.SetActive(true);
        if (targetObject[i] != null)
        {
            runToTarget();
        }
    }

    public void turnIntoObject()
    {
        assetToHide = GameObject.Find("Assets");
        for(int j=0; j<3; j++){
            childCount = assetToHide.transform.childCount;
            targetObject[j] = assetToHide.transform
                .GetChild(Random.Range(0, childCount))
                .gameObject.transform;
            targetObject[j].transform.parent = gameObject.transform;
            targetObject[j].gameObject.AddComponent<craneObject>();
            targetObject[j].gameObject.tag = "HiddingObject";
            craneObject = targetObject[j].GetComponent<craneObject>();
        }

        craneAnim.SetBool("isWalking", false);
    }

    private void resetObject()
    {
        for(int j=0; j<3; j++){
            targetObject[j].gameObject.SetActive(true);
            targetObject[j].gameObject.layer = 0;
            targetObject[j].gameObject.GetComponent<Renderer>().materials = mats;
            Destroy(targetObject[j].gameObject.GetComponent<PostProcessVolume>());
            Destroy(targetObject[j].gameObject.GetComponent<craneObject>());
            targetObject[j].transform.parent = GameObject.Find("Assets").transform;
        }
    }

    private void changeMaterial()
    {
        targetObject[i].gameObject.layer = 11;
        mats = targetObject[i].gameObject.GetComponent<Renderer>().materials;
        targetObject[i].gameObject.GetComponent<Renderer>().material = newMaterialRef;
        targetObject[i].gameObject.AddComponent<PostProcessVolume>();
        targetObject[i].gameObject.GetComponent<PostProcessVolume>().profile = postProcessProfile;
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

    public void MissionComplete()
    {
        npcCrane.npcState = 6;
        npcCrane.missionComplete = true;
        Timer.setTimeToPause();
        GameUI.SetActive(false);
    }

    public void GameComplete()
    {
        Timer.setTimeToPause();
        GameUI.SetActive(false);
        npcCrane.agent.destination = new Vector3(20, 3, -10);
        Crane.transform.GetChild(1).gameObject.SetActive(true);
        npcCrane.gameComplete = true;
    }

    public void resetGame()
    {
        Crane.transform.position = new Vector3(20, 3, -10);
        npcCrane.agent.destination = new Vector3(20, 3, -10);
        Crane.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        Crane.transform.GetChild(1).gameObject.SetActive(true);
        Debug.Log("reset");
        craneAnim.SetBool("isWalking", false);
        resetObject();
        npcCrane.npcState = 5;
    }

    public void runToTarget()
    {
        npcCrane.agent.speed = 20;
        npcCrane.agent.destination = targetObject[i].position;
        craneAnim.SetBool("isWalking", true);
    }
}
