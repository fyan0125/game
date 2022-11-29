using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSound : MonoBehaviour
{
    private void ChickenCrow()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
