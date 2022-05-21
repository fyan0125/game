using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPortal : MonoBehaviour
{
    public GameObject Portal;
    
    public bool isClear;
    // Start is called before the first frame update
    void Start()
    {
        isClear = false;
        //Portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear){
            Portal.SetActive(true);
        }else if(!isClear){
            Portal.SetActive(false);
        }
    }
}
