using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static bool soundGrounded;

    private void Step()
    {
        if (soundGrounded)
        {
            AudioManager.instance.Play("Step");
        }
    }

    private void Swing()
    {
        AudioManager.instance.Play("Swing");
    }

    private void Fall()
    {
        AudioManager.instance.Play("Fall");
    }

    private void Shock()
    {
        AudioManager.instance.Play("Shock");
    }
}
