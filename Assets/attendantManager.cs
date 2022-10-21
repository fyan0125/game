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

    private void Start()
    {
        chooseBT.onClick.AddListener(ChangeAttendant);
    }

    public void ChangeAttendant()
    {
        if (rabbitArea.activeSelf)
        {
            Debug.Log("rabbitArea");
            rabbit.SetActive(true);
            wolf.SetActive(false);
            fox.SetActive(false);
        }
        else if (wolfArea.activeSelf)
        {
            Debug.Log("wolfArea");
            rabbit.SetActive(false);
            wolf.SetActive(true);
            fox.SetActive(false);
        }
        else if (foxArea.activeSelf)
        {
            Debug.Log("foxArea");
            rabbit.SetActive(false);
            wolf.SetActive(false);
            fox.SetActive(true);
        }
        else
        {
            Debug.Log("nothing");
            rabbit.SetActive(false);
            wolf.SetActive(false);
            fox.SetActive(false);
        }
    }
}
