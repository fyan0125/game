using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    private void Fly()
    {
        AudioManager.instance.Play("Fly");
    }
}
