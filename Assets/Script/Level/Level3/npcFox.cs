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
    public Button foxbtn;
    public Button memorybtn;
    public GameObject foxIcon;
    public GameObject memoryIcon;
    public GameObject notice;
    public GameObject counter;
    public GameObject compoundLock;

    //傳送門
    public GameObject SendPoint;
    private showPortal sP;

    private void Start()
    {
        base.Start();
        SwitchSkills.getSkill = 2;
        sP = SendPoint.GetComponent<showPortal>();
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
                Debug.Log("Complete");
                foxbtn.interactable = true;
                foxIcon.SetActive(false);
                memorybtn.interactable = true;
                memoryIcon.SetActive(true);
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
