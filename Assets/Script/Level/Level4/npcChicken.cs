using UnityEngine;

public class npcChicken : DialogueTrigger
{
    public static bool gameComplete = false;
    public Conversation convo1;
    public Conversation convo2;

    private SkillUI skillUI;

    public GameObject SendPoint;
    private showPortal sP;

    private void Awake()
    {
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        gameObject.tag = null;
    }

    private void Update()
    {
        if (gameComplete && npcState == 1)
        {
            gameObject.tag = "NPC";
            npcState = 2;
        }

        if (npcState == 3 && DialogueManager.EndConversation())
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
                break;
            default:
                convo = convo1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }
}
