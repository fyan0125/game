using UnityEngine;

public class npcChicken : DialogueTrigger
{
    public static bool gameComplete = false;
    public Conversation convo1;
    public Conversation convo2;
    public Conversation defaultConvo;

    private SkillUI skillUI;

    public GameObject SendPoint;
    private showPortal sP;

    private void Awake()
    {
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        // notificationTrigger.EndNotice();
    }

    private void Update()
    {
        if (gameComplete && npcState == 1)
        {
            gameObject.tag = "NPC";
            npcState = 2;
            notificationTrigger.Notice("任務完成！回去神社前看看神雞有甚麼話要說。");
        }

        if (npcState == 3 && !DialogueManager.isTalking)
        {
            skillUI.ClearLevel(4);
            NpcReward.GetReward();
            SwitchSkills.getSkill = 3;
            notificationTrigger.Notice();
            npcState++;
            sP.isClear = true;
        }

        if (npcState > 3 && Input.GetButtonDown("SwitchSkills"))
        {
            notificationTrigger.EndNotice();
        }
    }

    public override void StartConvo()
    {
        switch (npcState)
        {
            case 1:
                convo = convo1;
                break;
            case 2:
                convo = convo2;
                npcState += 1;
                notificationTrigger.EndNotice();
                break;
            default:
                convo = defaultConvo;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
