using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private Animator anim;
    private SwitchSkills switchSkills;
    public GameObject weapon;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        switchSkills = GetComponent<SwitchSkills>();
    }

    void Update()
    {
        if (switchSkills.currentSkill == 2)
        {
            if (weapon.activeSelf == false)
                weapon.SetActive(true);

            if (Input.GetButtonDown("Skill"))
                Melee();
        }
        else
        {
            anim.SetInteger("currentSkill", 0);
            if (weapon.activeSelf == true)
                weapon.SetActive(false);
        }
    }

    void Melee()
    {
        anim.SetTrigger("Melee");
    }
}
