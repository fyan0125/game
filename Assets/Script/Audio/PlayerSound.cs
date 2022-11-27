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
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Step");
        }
    }

    private void Swing()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Swing");
    }

    private void Fall()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Fall");
    }
}
