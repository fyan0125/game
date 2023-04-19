using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attendantManager : MonoBehaviour
{
    public Button chooseBT;
    public GameObject rabbitArea;
    public GameObject wolfArea;
    public GameObject foxArea;
    public GameObject chickenArea;
    public GameObject craneArea;
    public GameObject deerArea;
    private GameObject deer;
    public GameObject crowArea;
    private GameObject followPlayer;
    private FollowPlayer fP;

    private void Start()
    {
        chooseBT.onClick.AddListener(ChangeAttendant);
        followPlayer = GameObject.Find("Following");
        fP = followPlayer.GetComponent<FollowPlayer>();
    }

    private void Update()
    {
        if (!deer)
        {
            if (GameObject.Find("Player/Mount").transform.childCount > 0)
            {
                deer = GameObject.Find("Player/Mount").transform.GetChild(0).gameObject;
            }
        }
    }

    public void ChangeAttendant()
    {
        if (rabbitArea.activeSelf)
        {
            Debug.Log("rabbitArea");
            if (deer)
                deer.SetActive(false);
            fP.choosed[0] = !fP.choosed[0];
            if (fP.choosed[0])
            {
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (wolfArea.activeSelf)
        {
            Debug.Log("wolfArea");
            if (deer)
                deer.SetActive(false);
            fP.choosed[1] = !fP.choosed[1];
            if (fP.choosed[1])
            {
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (foxArea.activeSelf)
        {
            Debug.Log("foxArea");
            if (deer)
                deer.SetActive(false);
            fP.choosed[2] = !fP.choosed[2];
            if (fP.choosed[2])
            {
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (chickenArea.activeSelf)
        {
            Debug.Log("chickenArea");
            if (deer)
                deer.SetActive(false);
            fP.choosed[3] = !fP.choosed[3];
            if (fP.choosed[3])
            {
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (craneArea.activeSelf)
        {
            Debug.Log("craneArea");
            if (deer)
                deer.SetActive(false);
            fP.choosed[4] = !fP.choosed[4];
            if (fP.choosed[4])
            {
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (deerArea.activeSelf)
        {
            Debug.Log("deer");
            if (deer)
                deer.SetActive(true);
            followPlayer.SetActive(false);
        }
        else
        {
            Debug.Log("nothing");
            followPlayer.SetActive(true);
        }
    }
}
