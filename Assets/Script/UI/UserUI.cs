using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UserUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject packageUI;
    public GameObject settingUI;
    public GameObject mapUI;
    public Transform itemsParent;

    private void Awake()
    {
        mapUI = GameObject.Find("Map");
        packageUI.SetActive(false);
        settingUI.SetActive(false);
        //mapUI.SetActive(false);
    }

    void Update()
    {
        if (!DialogueManager.isTalking)
        {
            if (Input.GetButtonDown("Inventory") && !settingUI.activeSelf && !mapUI.activeSelf)
            {
                packageUI.SetActive(!packageUI.activeSelf);

                if (GameIsPaused)
                    Resume();
                else
                    Pause();
            }
            else if (Input.GetButtonDown("Pause") && !packageUI.activeSelf && !mapUI.activeSelf)
            {
                settingUI.SetActive(!settingUI.activeSelf);

                if (GameIsPaused)
                    Resume();
                else
                    Pause();
            }
            else if (Input.GetButtonDown("Map") && !packageUI.activeSelf && !settingUI.activeSelf)
            {
                mapUI.SetActive(!mapUI.activeSelf);

                if (GameIsPaused)
                    Resume();
                else
                    Pause();
            }
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
