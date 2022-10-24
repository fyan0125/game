using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    protected Conversation convo;
    public int npcState = 1;
    protected npcReward NpcReward;
    protected NotificationTrigger notificationTrigger;

    public virtual void Start()
    {
        NpcReward = GetComponent<npcReward>();
        notificationTrigger = GetComponent<NotificationTrigger>();
    }

    public virtual void StartConvo()
    {
        DialogueManager.StartConversation(convo);
    }
}
