using UnityEngine;
using UnityEngine.AI;

public class npcFox : DialogueTrigger
{
    public Conversation convo1;
    public Conversation convo2;
    public Conversation convo3;

    // UI介面控制
    private SkillUI skillUI;
    public GameObject notice;
    public GameObject counter;
    public GameObject compoundLock;

    //傳送門
    public GameObject SendPoint;
    private showPortal sP;

    private ThirdPersonChar player;

    private NavMeshAgent navMeshAgent;
    public Transform target;
    public LayerMask playerLayer;
    public LayerMask targetLayer;
    public float nearTarget = 3f;

    public override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(-38f, 19.7f, -25.8f), new Vector3(0, 180, 0));

        SwitchSkills.getSkill = 2;
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        notice = GameObject.Find("/ObjectToNextLevel/Canvas/Notification/Notice");
        counter = GameObject.Find("/ObjectToNextLevel/Canvas/Notification/Counter");
        compoundLock = GameObject.Find("/ObjectToNextLevel/Canvas/Package/Panel/Compound/lock");
        counter.SetActive(true);
        notice.SetActive(false);
    }

    private void Update()
    {
        bool isNearTarget =
            npcState == 1
                ? Physics.CheckSphere(transform.position, nearTarget, playerLayer)
                : Physics.CheckSphere(transform.position, nearTarget, targetLayer);

        //任務條件
        if (NotificationManager.instance.count >= 10)
        {
            counter.SetActive(false);
            notice.SetActive(true);
            npcState = 3;
            NotificationManager.instance.count = -100;
        }

        if (npcState == 1)
        {
            if (!isNearTarget)
            {
                ChaseTarget(player.transform.position);
            }
            else
            {
                navMeshAgent.speed = 0;
            }
        }

        if (npcState == 2 && DialogueManager.EndConversation())
        {
            notificationTrigger.Notice();
            if (!isNearTarget)
            {
                ChaseTarget(target.position);
            }
            else
            {
                navMeshAgent.speed = 0;
            }
        }
        else if (npcState == 4)
        {
            notificationTrigger.EndNotice();
            if (DialogueManager.EndConversation())
            {
                skillUI.ClearLevel(3);
                compoundLock.SetActive(false);
                NpcReward.GetReward();
                npcState++;
                sP.isClear = true;
            }
        }
    }

    public override void StartConvo()
    {
        switch (npcState)
        {
            case 1:
                convo = convo1;
                npcState += 1;
                break;
            case 2:
                convo = convo2;
                break;
            case 3:
                convo = convo3;
                npcState += 1;
                break;
            default:
                convo = convo1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }

    private void ChaseTarget(Vector3 target = default(Vector3))
    {
        navMeshAgent.speed = 5;
        navMeshAgent.destination = target;
    }
}
