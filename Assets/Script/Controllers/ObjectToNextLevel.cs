using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ObjectToNextLevel : MonoBehaviour
{
    public static ObjectToNextLevel instance;

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
}
