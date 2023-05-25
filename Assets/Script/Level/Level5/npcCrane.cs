using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcCrane : DialogueTrigger
{
    public bool gameComplete = false,
        missionComplete = false;
    public Conversation convo1,
        convo2,
        convo3;
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

    private bool missionOK = false;

    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(8.12f, 4.4f, -10.534f), new Vector3(0, 90, 0));
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Level5Manager = GameObject.Find("Level5Manager").GetComponent<level5Manager>();
        timee = Timer.time;
        // notificationTrigger.EndNotice();
    }

    private void Update()
    {
        checkState();
    }

    public void checkState()
    {
        if (Timer.timerIsRunning)
        {
            if (Timer.timeRemaining <= 0)
            {
                timee = 0;
            }
        }
        if (npcState == 2 && DialogueManager.isTalking == false && !gameComplete && !missionOK) //對話後遊戲開始
        {
            npcState += 1;
            timee = Timer.time;
            Debug.Log("Start");
            Level5Manager.i = 0;
            Level5Manager.GameStart();
            Timer.setTimeToDisplay();
            missionOK = true;
        }
        if (missionComplete) //分次任務完成
        {
            Timer.timeRemaining = Timer.time;
            Level5Manager.i += 1;
            npcState = 3;
            StartConvo();
            Level5Manager.GameStart();
            Timer.setTimeToDisplay();
            missionComplete = false;
        }
        if (gameComplete)
        {
            gameObject.tag = "NPC";
            npcState = 4;
        }
        if (npcState == 3 && !gameComplete && timee == 0) //時間到未完成遊戲
        {
            Level5Manager.resetGame();
            resetGame = true;
            notificationTrigger.Notice();
        }
        if (npcState == 4 && DialogueManager.EndConversation())
        {
            skillUI.ClearLevel(5);
            SwitchSkills.getSkill = 4;
            npcState++;
            sP.isClear = true;
            NpcReward.GetReward();
        }
        if (npcState == 5 && Input.GetButtonDown("Skill")) //關閉失敗通知
        {
            npcState = 1;
            notificationTrigger.EndNotice();
            resetGame = false;
            missionOK = false;
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
