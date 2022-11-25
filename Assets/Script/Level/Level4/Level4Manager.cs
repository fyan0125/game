using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level4Manager : MonoBehaviour
{
    public GameObject BillBoardUI;
    public GameObject chickenPrefab;
    public int generateChickenNum;
    public int chickenNum = 10;
    public float time = 90;

    [Header("遊戲進行中")]
    public int catchedChickenNum = 0;
    public TextMeshProUGUI message;

    private ThirdPersonChar player;

    public GameObject[] places;

    private void Start()
    {
        BillBoardUI.SetActive(false);
        SwitchSkills.getSkill = 0;
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        Timer.time = time;
    }

    private void Update()
    {
        if (BillBoardUI.activeSelf == true)
        {
            Pause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player") && BillBoardUI.activeSelf == false)
        {
            BillBoardUI.SetActive(true);
        }
    }

    public void GameStart()
    {
        Resume();
        player.MoveToTarget(new Vector3(17, -9, -24));

        //隨機生成雞
        List<int> listNumbers = new List<int>();
        int number;
        for (int i = 0; i < generateChickenNum; i++)
        {
            do
            {
                number = Random.Range(0, places.Length);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }

        for (int i = 0; i < generateChickenNum; i++)
        {
            Instantiate(
                chickenPrefab,
                places[listNumbers[i]].transform.position,
                Quaternion.identity
            );
        }
        gameObject.SetActive(false);
        message.text =
            "總共抓到" + catchedChickenNum + "隻雞，還差" + (chickenNum - catchedChickenNum) + "隻";
    }

    public void CatchedChicken()
    {
        catchedChickenNum++;
        message.text =
            "總共抓到" + catchedChickenNum + "隻雞，還差" + (chickenNum - catchedChickenNum) + "隻";
        if (catchedChickenNum == chickenNum)
        {
            Timer.setTimeToPause();
            GameComplete();
        }
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void GameComplete()
    {
        Debug.Log("Level4 Complete");
        SwitchSkills.getSkill = 2;
        npcChicken.gameComplete = true;
    }
}
