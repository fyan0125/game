using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public Conversation convo;
    public int npcState = 1;

    public virtual void StartConvo()
    {
        // DialogueManager.StartConversation(convo);
    }
}
