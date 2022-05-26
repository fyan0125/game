using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    protected Conversation convo;
    public int npcState = 1;
    protected npcReward NpcReward;

    public virtual void Start()
    {
        NpcReward = GetComponent<npcReward>();
    }

    public virtual void StartConvo()
    {
        // DialogueManager.StartConversation(convo);
    }
}
