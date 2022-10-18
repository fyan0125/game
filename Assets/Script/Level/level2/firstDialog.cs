using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstDialog : DialogueTrigger
{
    public Conversation convo1;

    public override void Start() {
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
        if(npcState == 1){
            npcState++;
        }
        DialogueManager.StartConversation(convo1);
    }
}