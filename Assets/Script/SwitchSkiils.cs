using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkiils : MonoBehaviour
{
    int[] skills = new int[4] { 0, 1, 2, 3 };
    public int currentSkill = 0;

    void Start()
    {
        Debug.Log("Rabbit");
    }

    void Update()
    {
        if (Input.GetButtonDown("SwitchSkiils"))
        {
            switch (currentSkill)
            {
                case 0:
                    currentSkill = skills[1];
                    Debug.Log("Wolf");
                    break;
                case 1:
                    currentSkill = skills[2];
                    Debug.Log("Chicken");
                    break;
                case 2:
                    currentSkill = skills[3];
                    Debug.Log("Crane");
                    break;
                case 3:
                    currentSkill = skills[0];
                    Debug.Log("Rabbit");
                    break;
                default:
                    Debug.Log("Default case");
                    break;
            }
        }
    }
}
