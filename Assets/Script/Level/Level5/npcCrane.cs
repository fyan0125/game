using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcCrane : DialogueTrigger
{
    public static bool gameComplete = false;
    public Conversation convo1, convo2, convo3;
    public UnityEngine.AI.NavMeshAgent agent;

    public Button cranebtn;
    public Button memorybtn;
    public GameObject craneIcon;
    public GameObject memoryIcon;

    public level5Manager Level5Manager;

    public GameObject SendPoint;
    private showPortal sP;

    private bool resetGame = false;
    public float timee;

    private void Awake()
    {
        base.Start();
        sP = SendPoint.GetComponent<showPortal>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Level5Manager = GameObject.Find("Level5Manager").GetComponent<level5Manager>();
        timee = Timer.time;
    }

    private void Update()
    {   
        if(Timer.timerIsRunning){
            if(Timer.timeRemaining <= 0){
                timee = 0;
            }                                                 
        }
        if(npcState == 1 && Input.GetButtonDown("Skill") && resetGame){
            notificationTrigger.EndNotice();
            resetGame = false;
        }
        if(npcState == 2 && DialogueManager.isTalking == false && !gameComplete){
            Level5Manager.GameStart();
            Timer.setTimeToDisplay();
            npcState += 1;
        }
        if(gameComplete){
            npcState = 4;
        }
        if(!gameComplete && timee == 0 && npcState == 3)
        {
            Level5Manager.resetGame();
            resetGame = true;
            notificationTrigger.Notice();
            npcState = 1;
        }
        if (npcState == 5 && DialogueManager.EndConversation())
        {
            cranebtn.interactable = true;
            craneIcon.SetActive(false);
            memorybtn.interactable = true;
            memoryIcon.SetActive(true);
            NpcReward.GetReward();
            SwitchSkills.getSkill = 4;
            //notificationTrigger.Notice();
            npcState++;
            sP.isClear = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player") && npcState == 1)
        {
            StartConvo();
        }
    }
    
    public override void StartConvo()
    {
        switch (npcState)
        {
            case 1:
                convo = convo1;
                npcState += 1;
                break;
            case 4:
                convo = convo3;
                npcState += 1;
                break;
            default:
                convo = convo1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
