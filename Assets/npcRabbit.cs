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
}
