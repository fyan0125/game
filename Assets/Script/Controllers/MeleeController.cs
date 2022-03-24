using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public Animator animator;
    private SwitchSkiils switchSkiils;

    void Start()
    {
        switchSkiils = GetComponent<SwitchSkiils>();
    }

    void Update()
    {
        if (switchSkiils.currentSkill == 1)
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
