using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time = 10;
    private float timeRemaining;
    public static bool timerIsRunning = false;
    private TextMeshProUGUI timeText;

    [Header("Level4")]
    public GameObject Level4UI;
    public GameObject GameUI;
    public GameObject BillBoardUI;
    public GameObject billBoard;

    private void Start()
    {
        timeRemaining = time;
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = time;
                timerIsRunning = false;
                timeText.text = "00:00";
                billBoard.SetActive(true);
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");
                    foreach (GameObject chicken in chickens)
                        GameObject.Destroy(chicken);
                    Level4UI.SetActive(false);
                    GameUI.SetActive(false);
                    BillBoardUI.SetActive(true);
                }
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void setTimeToDisplay()
    {
        timerIsRunning = true;
    }

    public static void setTimeToPause()
    {
        timerIsRunning = false;
    }
}
