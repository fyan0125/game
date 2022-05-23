using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcRabbit : DialogueTrigger
{
    public Button rabbitbtn;
    public Button memorybtn;
    public GameObject rabbitIcon;
    public GameObject memoryIcon;

    public Dialogue dialogue1;
    public Dialogue dialogue2;

    private void Update()
    {
        if (npcState == 3)
        {
            rabbitbtn.interactable = true;
            rabbitIcon.SetActive(false);
            memorybtn.interactable = true;
            memoryIcon.SetActive(true);
            SwitchSkills.getSkill = 1;
        }
    }

    public override void TriggerDialogue()
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
