using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcRabbit : DialogueTrigger
{
    public Conversation convo1;
    public Conversation convo2;

    public Button rabbitbtn;
    public Button memorybtn;
    public GameObject rabbitIcon;
    public GameObject memoryIcon;

    private void Update()
    {
        if (npcState == 3 && DialogueManager.EndConversation())
        {
            rabbitbtn.interactable = true;
            rabbitIcon.SetActive(false);
            memorybtn.interactable = true;
            memoryIcon.SetActive(true);
            SwitchSkills.getSkill = 1;
            NpcReward.GetReward();
            notificationTrigger.Notice();
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
                break;
            case 2:
                convo = convo2;
                npcState += 1;
                break;
            default:
                convo = convo1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
