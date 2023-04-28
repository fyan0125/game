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
    private GameObject crow;
    private GameObject followPlayer;
    private FollowPlayer fP;
    public Mount mount;

    private void Start()
    {
        chooseBT.onClick.AddListener(ChangeAttendant);
        followPlayer = GameObject.Find("Following");
        fP = followPlayer.GetComponent<FollowPlayer>();
        mount = GameObject.Find("Player").GetComponentInChildren<Mount>();
    }

    private void Update()
    {
        if (!deer)
        {
            deer = mount.Deer;
        }
        if (!crow)
        {
            crow = mount.Yatagarasu;
        }
    }

    public void ChangeAttendant()
    {
        if (rabbitArea.activeSelf)
        {
            Debug.Log("rabbitArea");
            if (deer)
                deer.SetActive(false);
            changeGodManager(0);
        }
        else if (wolfArea.activeSelf)
        {
            Debug.Log("wolfArea");
            if (deer)
                deer.SetActive(false);
            changeGodManager(1);
        }
        else if (foxArea.activeSelf)
        {
            Debug.Log("foxArea");
            if (deer)
                deer.SetActive(false);
            changeGodManager(2);
        }
        else if (chickenArea.activeSelf)
        {
            Debug.Log("chickenArea");
            if (deer)
                deer.SetActive(false);
            changeGodManager(3);
        }
        else if (craneArea.activeSelf)
        {
            Debug.Log("craneArea");
            if (deer)
                deer.SetActive(false);
            changeGodManager(4);
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

    public void changeGodManager(int k)
    {
        fP.choosed[k] = !fP.choosed[k];
        fP.nowFollowing();
        if (fP.choosed[k])
        {
            fP.startFollowing();
        }
    }
}
