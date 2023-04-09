using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attendantManager : MonoBehaviour
{
    public Button chooseBT;
    public GameObject rabbitArea;
    public GameObject rabbit;
    public GameObject wolfArea;
    public GameObject wolf;
    public GameObject foxArea;
    public GameObject fox;
    public GameObject deerArea;
    private GameObject deer;
    private GameObject followPlayer;

    private void Start()
    {
        chooseBT.onClick.AddListener(ChangeAttendant);
        followPlayer = GameObject.Find("Following");
        followPlayer.SetActive(false);
    }

    private void Update()
    {
        if (!deer)
        {
            if (GameObject.Find("Player/Deer").transform.childCount > 0)
            {
                deer = GameObject.Find("Player/Deer").transform.GetChild(0).gameObject;
            }
        }
    }

    public void ChangeAttendant()
    {
        if (rabbitArea.activeSelf)
        {
            Debug.Log("rabbitArea");
            rabbit.SetActive(true);
            wolf.SetActive(false);
            fox.SetActive(false);
            if (deer)
                deer.SetActive(false);
            followPlayer.SetActive(true);
        }
        else if (wolfArea.activeSelf)
        {
            Debug.Log("wolfArea");
            rabbit.SetActive(false);
            wolf.SetActive(true);
            fox.SetActive(false);
            if (deer)
                deer.SetActive(false);
            followPlayer.SetActive(true);
        }
        else if (foxArea.activeSelf)
        {
            Debug.Log("foxArea");
            rabbit.SetActive(false);
            wolf.SetActive(false);
            fox.SetActive(true);
            if (deer)
                deer.SetActive(false);
            followPlayer.SetActive(true);
        }
        else if (deerArea.activeSelf)
        {
            Debug.Log("deer");
            rabbit.SetActive(false);
            wolf.SetActive(false);
            fox.SetActive(false);
            if (deer)
                deer.SetActive(true);
            followPlayer.SetActive(false);
        }
        else
        {
            Debug.Log("nothing");
            rabbit.SetActive(false);
            wolf.SetActive(false);
            fox.SetActive(false);
            followPlayer.SetActive(true);
        }
    }
}
