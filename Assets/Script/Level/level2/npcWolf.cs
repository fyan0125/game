using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcWolf : DialogueTrigger
{
    public Conversation convo1;
    public Conversation defaultConvo;

    // UI介面控制
    public GameObject SendPoint;
    private showPortal sP;

    private SkillUI skillUI;

    public override void Start()
    {
        base.Start();
        SwitchSkills.getSkill = 1;
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        sP = SendPoint.GetComponent<showPortal>();
    }

    private void Update()
    {
        if (npcState == 2 && !DialogueManager.isTalking)
        {
            skillUI.ClearLevel(2);
            SwitchSkills.getSkill = 2;
            NpcReward.GetReward();
            notificationTrigger.Notice();
            npcState++;
            sP.isClear = true;
        }
        else if (npcState == 3 && Input.GetButtonDown("SwitchSkills"))
        {
            notificationTrigger.EndNotice();
            npcState++;
        }
    }

    public override void StartConvo()
    {
        switch (npcState)
        {
            case 1:
                convo = convo1;
                npcState += 1;
                notificationTrigger.EndNotice();
                break;
            default:
                convo = defaultConvo;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
