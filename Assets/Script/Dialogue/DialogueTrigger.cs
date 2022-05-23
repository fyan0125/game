using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;

    private Dialogue dialogue;
    public int npcState = 1;

    public void TriggerDialogue()
    {
        switch (npcState)
        {
            case 1:
                dialogue = dialogue1;
                npcState += 1;
                break;
            case 2:
                dialogue = dialogue2;
                npcState += 1;
                break;
            default:
                dialogue = dialogue2;
                break;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}