using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;

    private Dialogue dialogue;
    private int npcState = 1;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))//任務完成
        {
            npcState += 1;
        }
    }

    public void TriggerDialogue()
    {
        switch (npcState)
        {
            case 1:
                dialogue = dialogue1;
                break;
            case 2:
                dialogue = dialogue2;
                break;
            default:
                dialogue = dialogue1;
                break;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}