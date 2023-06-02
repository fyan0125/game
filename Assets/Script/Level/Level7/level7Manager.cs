using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level7Manager : DialogueTrigger
{
    private Animator anim;
    public Conversation convo1, convo2;

    private NotificationTrigger notificationTrigger2;

    private SkillUI skillUI;
    private Mount mount;
    
    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Grounded", true);
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        mount = GameObject.Find("Mount").GetComponent<Mount>();
        notificationTrigger2 = gameObject.transform.GetChild(0).gameObject.GetComponent<NotificationTrigger>();
    }

    private void Update()
    {
        if (npcState == 2 && !DialogueManager.isTalking)
        {
            notificationTrigger.Notice();
            npcState++;
        }
        else if (npcState == 3 && Input.GetButtonDown("Inventory"))
        {
            npcState++;
        }
        else if (npcState == 4 && Input.GetButtonDown("Inventory"))
        {
            notificationTrigger.EndNotice();
            StartConvo();
        }
        else if (npcState == 5 && !DialogueManager.isTalking)
        {
            notificationTrigger2.Notice();
            skillUI.ClearLevel(7);
            npcState++;
        }
        else if (npcState == 6 && Input.GetButtonDown("Skill"))
        {
            notificationTrigger2.EndNotice();
            npcState++;
            mount.crowActive = true;
            gameObject.SetActive(false);
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
                convo = convo2;
                npcState += 1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
