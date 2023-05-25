using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
