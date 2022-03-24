using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetButtonDown("Melee"))
        {
            Melee();
        }
    }

    void Melee()
    {
        animator.SetTrigger("Melee");
    }

}
