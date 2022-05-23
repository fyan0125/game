using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkills : MonoBehaviour
{
    public static int getSkill = 0;
    public int currentSkill = 0;

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
    }
}
