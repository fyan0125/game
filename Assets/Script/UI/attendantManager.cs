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
    private GameObject followPlayer;
    private FollowPlayer fP;

    private void Start()
    {
        chooseBT.onClick.AddListener(ChangeAttendant);
        followPlayer = GameObject.Find("Following");
        fP = followPlayer.GetComponent<FollowPlayer>();
    }

    public void ChangeAttendant()
    {
        if (rabbitArea.activeSelf)
        {
            Debug.Log("rabbitArea");
            rabbit.SetActive(true);
            wolf.SetActive(false);
            fox.SetActive(false);
            fP.choosed[0] = !fP.choosed[0];
            if(fP.choosed[0]){
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (wolfArea.activeSelf)
        {
            Debug.Log("wolfArea");
            rabbit.SetActive(false);
            wolf.SetActive(true);
            fox.SetActive(false);
            fP.choosed[1] = !fP.choosed[1];
            if(fP.choosed[1]){
                fP.startFollowing();
            }
            fP.nowFollowing();
        }
        else if (foxArea.activeSelf)
        {
            Debug.Log("foxArea");
            rabbit.SetActive(false);
            wolf.SetActive(false);
            fox.SetActive(true);
            fP.choosed[2] = !fP.choosed[2];
            if(fP.choosed[2]){
                fP.startFollowing();
            }
            fP.nowFollowing();
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
