using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public Animator animator;
    private SwitchSkills SwitchSkills;

    void Start()
    {
        SwitchSkills = GetComponent<SwitchSkills>();
    }

    void Update()
    {
        if (SwitchSkills.currentSkill == 2)
        {
            if (Input.GetButtonDown("Skill"))
            {
                Melee();
            }
        }
    }

    void Melee()
    {
        animator.SetTrigger("Melee");
    }
}
