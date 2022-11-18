using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time = 10;
    private float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;

    [Header("Level4")]
    public GameObject Level4UI;
    public GameObject TimerUI;
    public GameObject BillBoardUI;

    private void Start()
    {
        timeRemaining = time;
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
                gameObject.SetActive(false);
                timeText.text = "";
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");
                    foreach (GameObject chicken in chickens)
                        GameObject.Destroy(chicken);
                    Level4UI.SetActive(false);
                    TimerUI.SetActive(true);
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

    public void setTimeToDisplay()
    {
        timerIsRunning = true;
    }
}
