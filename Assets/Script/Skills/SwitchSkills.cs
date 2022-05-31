using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkills : MonoBehaviour
{
    public static int getSkill = 3;
    public int currentSkill = 0;
    public GameObject RabbitSkill;

    void Update()
    {
        if (Input.GetButtonDown("SwitchSkills"))
        {
            if (currentSkill < getSkill)
            {
                currentSkill += 1;
            }
            else currentSkill = 0;
        }

        switch (currentSkill)
        {
            case 1:
                RabbitSkill.SetActive(true);
                break;
            default:
                RabbitSkill.SetActive(false);
                break;
        }
    }
}
