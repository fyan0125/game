using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcChicken : DialogueTrigger
{
    public static bool gameComplete = false;
    public Conversation convo1;
    public Conversation convo2;

    public Button chickenbtn;
    public Button memorybtn;
    public GameObject chickenIcon;
    public GameObject memoryIcon;

    public GameObject SendPoint;
    private showPortal sP;

    private void Awake()
    {
        sP = SendPoint.GetComponent<showPortal>();
    }

    private void Update()
    {
        if (gameComplete && npcState == 1)
        {
            npcState = 2;
        }

        if (npcState == 3 && DialogueManager.EndConversation())
        {
            chickenbtn.interactable = true;
            chickenIcon.SetActive(false);
            memorybtn.interactable = true;
            memoryIcon.SetActive(true);
            NpcReward.GetReward();
            SwitchSkills.getSkill = 3;
            notificationTrigger.Notice();
            npcState++;
            sP.isClear = true;
        }

        if (npcState > 3 && Input.GetButtonDown("SwitchSkills"))
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
