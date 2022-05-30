using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTrigger : MonoBehaviour
{
    [TextArea(4, 4)]
    public string notice;

    public virtual void Notice()
    {
        NotificationManager.StartNotice(notice);
    }
}
