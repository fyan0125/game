using UnityEngine;
using UnityEngine.AI;

public class npcFox : DialogueTrigger
{
    [Tooltip("通關條件")]
    public int needToKill = 10;

    [Header("對話")]
    public Conversation convo1;
    public Conversation convo2;
    public Conversation convo3;
    public Conversation defaultConvo;

    // UI介面控制
    private SkillUI skillUI;
    private GameObject notice;
    private GameObject counter;
    private GameObject compoundLock;

    //傳送門
    public GameObject SendPoint;
    private showPortal sP;

    [HideInInspector]
    public ThirdPersonChar player;

    [Header("移動至目標")]
    private NavMeshAgent navMeshAgent;
    public Transform target;
    public LayerMask playerLayer;
    public LayerMask targetLayer;

    public Animator anim;

    public override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(-77.5f, 6.2f, 20f), new Vector3(0, 180, 0));

        SwitchSkills.getSkill = 2;
        sP = SendPoint.GetComponent<showPortal>();
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        notice = GameObject.Find("/ObjectToNextLevel/Canvas/Notification/Notice");
        counter = GameObject.Find("/ObjectToNextLevel/Canvas/Notification/Counter");
        compoundLock = GameObject
            .Find("ObjectToNextLevel")
            .transform.Find("Canvas/Package/Panel/Compound/lock")
            .gameObject;

        counter.SetActive(true);
        notice.SetActive(false);
    }

    private void Update()
    {
        bool isNearTarget =
            npcState == 1
                ? Physics.CheckSphere(transform.position, 3, playerLayer)
                : Physics.CheckSphere(transform.position, 1, targetLayer);

        //任務條件
        if (NotificationManager.instance.count >= needToKill)
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
                anim.SetBool("isWalking", true);
            }
        }

        if (npcState == 2 && !DialogueManager.isTalking)
        {
            notificationTrigger.Notice();
            if (!isNearTarget)
            {
                ChaseTarget(target.position);
                anim.SetBool("isWalking", true);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
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

        if (isNearTarget)
        {
            navMeshAgent.speed = 0;
            anim.SetBool("isWalking", false);
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
                convo = defaultConvo;
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
