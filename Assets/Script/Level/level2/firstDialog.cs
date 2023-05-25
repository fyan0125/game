using System.Collections;
using UnityEngine;

public class firstDialog : DialogueTrigger
{
    public Conversation convo1;
    private ThirdPersonChar player;

    public override void Start()
    {
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(17, 5, 77), new Vector3(0, 187, 0));
        StartConvo();
        notificationTrigger = GetComponent<NotificationTrigger>();
        // notificationTrigger.EndNotice();
    }

    private void Update()
    {
        if (npcState == 2 && DialogueManager.EndConversation())
        {
            notificationTrigger.Notice();
            npcState++;
        }
    }

    public override void StartConvo()
    {
        if (npcState == 1)
        {
            npcState++;
        }
        DialogueManager.StartConversation(convo1);
    }
}
