using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstDialog : DialogueTrigger
{
    public Conversation convo1;
    private ThirdPersonChar player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(17, 5, 77), new Vector3(0, 187, 0));
    }

    public override void Start()
    {
        StartConvo();
        notificationTrigger = GetComponent<NotificationTrigger>();
    }

    private void Update()
    {
        if (npcState == 2 && DialogueManager.EndConversation())
        {
            notificationTrigger.Notice();
            npcState++;
        }
    }

    public override void StartConvo()
    {
        if (npcState == 1)
        {
            npcState++;
        }
        DialogueManager.StartConversation(convo1);
    }
}
