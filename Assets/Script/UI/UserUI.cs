using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UserUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject packageUI;
    public GameObject settingUI;
    public Transform itemsParent;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            packageUI.SetActive(!packageUI.activeSelf);

            if (GameIsPaused) Resume();
            else Pause();
        }
        if (Input.GetButtonDown("Pause"))
        {
            settingUI.SetActive(!settingUI.activeSelf);

            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        settingUI.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
