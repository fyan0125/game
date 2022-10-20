using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkills : MonoBehaviour
{
    public static int getSkill = 0;
    public int currentSkill = 0;
    GameObject GameManager;
    SkillUI skillUI;

    public void Start()
    {
        GameManager = GameObject.Find("GameManager");
        skillUI = GameManager.GetComponent<SkillUI>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("SwitchSkills"))
        {
            if (currentSkill < getSkill)
            {
                currentSkill += 1;
            }
            else if (getSkill == 1)
            {
                currentSkill = 0;
            }
            else
                currentSkill = 1;
            skillUI.SkillUITransition(currentSkill);
        }
    }
}
