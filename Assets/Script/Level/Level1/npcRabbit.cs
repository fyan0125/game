using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcRabbit : DialogueTrigger
{
    public Conversation convo1;
    public Conversation convo2;
    public Conversation defaultConvo;

    private SkillUI skillUI;

    public GameObject SendPoint;
    private showPortal sP;

    private bool hasNotice = false; // 提醒重新回去找兔子

    private void Awake()
    {
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
    }

    private void Start()
    {
        base.Start();
        notificationTrigger.Notice("靠近神使時，使用R鍵可開啟對話。");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Talk") && npcState <= 3)
        {
            notificationTrigger.EndNotice();
        }

        if (npcState == 2 && DialogueManager.EndConversation() && !hasNotice)
        {
            hasNotice = !hasNotice;
            Invoke("StartConvo", 5.0f); //延遲3秒後執行setTimeOut
        }

        if (npcState == 3)
            CancelInvoke();

        if (npcState == 3 && DialogueManager.EndConversation())
        {
            skillUI.ClearLevel(1);
            SwitchSkills.getSkill = 1;
            NpcReward.GetReward();
            notificationTrigger.Notice();
            npcState++;
            sP.isClear = true;
        }

        if (npcState > 3 && Input.GetButtonDown("Inventory"))
        {
            notificationTrigger.EndNotice();
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
                npcState += 1;
                break;
            default:
                convo = defaultConvo;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
