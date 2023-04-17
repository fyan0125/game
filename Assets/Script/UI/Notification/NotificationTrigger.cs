using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTrigger : MonoBehaviour
{
    [TextArea(4, 4)]
    public string notice;

    public virtual void Notice(string newNotice = null)
    {
        if (newNotice == null)
        {
            newNotice = notice;
        }
        NotificationManager.StartNotice(newNotice);
    }

    public virtual void EndNotice()
    {
        NotificationManager.EndNotice();
    }
}
