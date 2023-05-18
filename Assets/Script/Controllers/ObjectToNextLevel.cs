using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ObjectToNextLevel : MonoBehaviour
{
    public static ObjectToNextLevel instance;
    public float skyboxRotate = 0.5f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotate);
    }
}
