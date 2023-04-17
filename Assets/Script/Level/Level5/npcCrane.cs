using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcCrane : DialogueTrigger
{
    public bool gameComplete = false, missionComplete= false;
    public Conversation convo1, convo2, convo3;
    public UnityEngine.AI.NavMeshAgent agent;

    // public Button cranebtn;
    // public Button memorybtn;
    // public GameObject craneIcon;
    // public GameObject memoryIcon;

    public level5Manager Level5Manager;

    public GameObject SendPoint;
    private showPortal sP;

    private bool resetGame = false;
    public float timee;

    private SkillUI skillUI;
    private ThirdPersonChar player;

    private void Start()
    {
        base.Start();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(8.12f, 4.4f, -10.534f), new Vector3(0, 90, 0));
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Level5Manager = GameObject.Find("Level5Manager").GetComponent<level5Manager>();
        timee = Timer.time;
    }

    private void Update()
    {   
        checkState();
    }

    public void checkState()
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
        if(npcState == 2 && DialogueManager.isTalking == false && !gameComplete){//對話後遊戲開始
            npcState += 1;
            Level5Manager.GameStart();
            Timer.setTimeToDisplay();
        }
        if(missionComplete){//分次任務完成
            StartConvo();
            Timer.timeRemaining = Timer.time;
            Level5Manager.i +=1 ;
            missionComplete = false;
        }
        if(gameComplete){
            npcState = 4;
        }
        if(!gameComplete && timee == 0) //時間到未完成遊戲
        {
            Level5Manager.resetGame();
            resetGame = true;
            notificationTrigger.Notice();
            npcState = 1;
        }
        if (npcState == 4 && DialogueManager.EndConversation())
        {
            skillUI.ClearLevel(5);
            NpcReward.GetReward();
            SwitchSkills.getSkill = 4;
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
            case 6:
                convo = convo2;
                npcState = 2;
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
