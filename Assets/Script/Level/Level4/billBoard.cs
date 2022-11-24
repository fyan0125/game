using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class billBoard : MonoBehaviour
{
    public GameObject Level4UI;
    public GameObject chickenPrefab;
    public int generateChickenNum;
    public int chickenNum = 10;

    [Header("遊戲進行中")]
    public int catchedChickenNum = 0;
    public TextMeshProUGUI message;

    public GameObject[] places;

    private void Start()
    {
        Level4UI.SetActive(false);
        SwitchSkills.getSkill = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player") && Level4UI.activeSelf == false)
        {
            Level4UI.SetActive(true);
        }
    }

    public void GameStart()
    {
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
            Level4UI.SetActive(false);
        }
    }
}
