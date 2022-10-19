using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public Text notification;
    public static NotificationManager instance;
    public Text count_text;

    [HideInInspector]
    public int count;

    public Animator anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartNotice(string notice)
    {
        instance.anim.SetTrigger("isOpened");
        instance.notification.text = notice;
    }

    public static void EndNotice()
    {
        instance.anim.SetTrigger("isClosed");
    }

    public void UpdateCount()
    {
        count_text.text = count.ToString();
    }
}
