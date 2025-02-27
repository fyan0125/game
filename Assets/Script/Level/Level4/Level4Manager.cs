using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Level4Manager : MonoBehaviour
{
    public GameObject BillBoardUI;
    public GameObject FailUI;
    public Button BillBoardUIStart;
    public Button FailUIStart;
    public Button FailUICancel;
    public TextMeshProUGUI message;

    public GameObject chickenPrefab;
    public int generateChickenNum;
    public int chickenNum = 10;
    public float time = 90;

    [Header("遊戲進行中")]
    public int catchedChickenNum = 0;

    private ThirdPersonChar player;

    public GameObject[] places;

    private void Start()
    {
        SetUpButton();

        BillBoardUI.SetActive(false);
        SwitchSkills.getSkill = 2;
        Timer.time = time;
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(17f, -9.4f, 13.7f), new Vector3(0, 180, 0));
    }

    // 玩家碰到告示牌
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player") && BillBoardUI.activeSelf == false)
        {
            BillBoardUI.SetActive(true);
            Pause();
        }
    }

    public void GameStart()
    {
        Resume();
        SwitchSkills.lockSkill = true;
        player.MoveToTarget(new Vector3(17, -9, -24));
        catchedChickenNum = 0;
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
                Quaternion.Euler(0, Random.Range(0f, 360f), 0),
                GameObject.Find("Chicken").transform
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
        Debug.Log("Level 4 Complete");
        SwitchSkills.lockSkill = false;
        SwitchSkills.getSkill = 2;
        npcChicken.gameComplete = true;
    }

    private void SetUpButton()
    {
        BillBoardUIStart.onClick.AddListener(GameStart);
        FailUIStart.onClick.AddListener(GameStart);
        FailUICancel.onClick.AddListener(Resume);
    }
}
