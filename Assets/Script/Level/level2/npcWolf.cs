using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcWolf : DialogueTrigger
{
    public Conversation convo1;

    // UI介面控制
    public Button wolfbtn;
    public Button memorybtn;
    public GameObject wolfIcon;
    public GameObject memoryIcon;

    public Animator anim;

    private void Update()
    {
        if (npcState == 2 && DialogueManager.EndConversation())
        {
            wolfbtn.interactable = true;
            wolfIcon.SetActive(false);
            memorybtn.interactable = true;
            memoryIcon.SetActive(true);
            SwitchSkills.getSkill = 2;
            NpcReward.GetReward();
            npcState++;
        }
    }

    public override void StartConvo()
    {
        convo = convo1;
        if(npcState == 1){
            npcState++;
        }
        DialogueManager.StartConversation(convo);
    }
}
