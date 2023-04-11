using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcFox : DialogueTrigger
{
    public Conversation convo1;
    public Conversation convo2;
    public Conversation convo3;

    // UI介面控制
    private SkillUI skillUI;
    public GameObject notice;
    public GameObject counter;
    public GameObject compoundLock;

    //傳送門
    public GameObject SendPoint;
    private showPortal sP;

    private ThirdPersonChar player;

    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(-18.7f, 8f, 83.69f), new Vector3(0, 180, 0));

        SwitchSkills.getSkill = 2;
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        notice = GameObject.Find("/ObjectToNextLevel/Canvas/Notification/Notice");
        counter = GameObject.Find("/ObjectToNextLevel/Canvas/Notification/Counter");
        compoundLock = GameObject.Find("/ObjectToNextLevel/Canvas/Package/Panel/Compound/lock");
        counter.SetActive(true);
        notice.SetActive(false);
    }

    private void Update()
    {
        //任務條件
        if (NotificationManager.instance.count >= 10)
        {
            counter.SetActive(false);
            notice.SetActive(true);
            npcState = 3;
            NotificationManager.instance.count = -100;
        }

        //關卡階段
        if (npcState == 2 && DialogueManager.EndConversation())
        {
            notificationTrigger.Notice();
        }
        else if (npcState == 4)
        {
            notificationTrigger.EndNotice();
            if (DialogueManager.EndConversation())
            {
                skillUI.ClearLevel(3);
                compoundLock.SetActive(false);
                NpcReward.GetReward();
                npcState++;
                sP.isClear = true;
            }
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
            case 2:
                convo = convo2;
                break;
            case 3:
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
