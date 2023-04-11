using UnityEngine;
using UnityEngine.AI;

//npcState == 1 對話 去找player
//npcState == 2 帶玩家走到鹿群旁邊
//npcState == 3 對話 叫玩家選一隻鹿
//npcState == 4 玩家選鹿
//npcState == 5 對話 玩家騎上鹿
public class level6Manager : DialogueTrigger
{
    public Conversation convo1;
    public Conversation convo2;
    public Conversation convo3;

    private ThirdPersonChar player;
    private SkillUI skillUI;
    private NavMeshAgent navMeshAgent;

    private Transform firstTarget;
    public Transform target;
    public float nearTarget;
    public LayerMask playerLayer;
    public LayerMask targetLayer;

    private GameObject level6UI;
    public GameObject chooseDeer;
    private bool getReward = false;

    private Animator anim;

    public override void Start()
    {
        base.Start();
        SwitchSkills.getSkill = 4;
        navMeshAgent = GetComponent<NavMeshAgent>();
        level6UI = GameObject.Find("Level6UI");
        if (level6UI != null)
        {
            level6UI.SetActive(false);
        }
        else
        {
            Debug.Log("can't find level 6 UI");
        }

        firstTarget = GameObject.Find("Player").transform;
        skillUI = GameObject.Find("GameManager").GetComponent<SkillUI>();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
        player.MoveToTarget(new Vector3(-36, 10, 61), new Vector3(0, 180, 0));
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        bool isNearTarget =
            npcState == 1
                ? Physics.CheckSphere(transform.position, nearTarget, playerLayer)
                : Physics.CheckSphere(transform.position, nearTarget, targetLayer);

        if (npcState == 1)
        {
            if (!isNearTarget)
            {
                ChaseTarget(firstTarget.position);
            }
            else
            {
                navMeshAgent.speed = 0;
            }
        }

        if (npcState == 2 && DialogueManager.EndConversation())
        {
            if (!isNearTarget)
            {
                ChaseTarget(target.position);
                npcState = 3;
            }
        }

        if (npcState == 3)
        {
            if (isNearTarget)
            {
                navMeshAgent.speed = 0;
                StartConvo();
            }
        }

        if (npcState >= 5 && getReward == false)
        {
            NpcReward.GetReward();
            npcState++;
            getReward = true;
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", navMeshAgent.speed);
        }
    }

    public override void StartConvo()
    {
        switch (npcState)
        {
            case 1:
                convo = convo1;
                npcState = 2;
                break;
            case 3:
                convo = convo2;
                npcState = 4;
                break;
            case 5:
                convo = convo3;
                npcState = 6;
                break;
            default:
                convo = convo1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }

    private void ChaseTarget(Vector3 target = default(Vector3))
    {
        navMeshAgent.speed = 8;
        navMeshAgent.destination = target;
    }

    public void ShowUI(GameObject deer)
    {
        level6UI.SetActive(true);
        chooseDeer = deer;
    }

    // finished 是否完成選擇
    public void DisableUI(bool finished = false)
    {
        level6UI.SetActive(false);
        chooseDeer = finished ? chooseDeer : null;
    }

    public void ConfirmDeer()
    {
        DisableUI(true);
        npcState = 5;
        chooseDeer.GetComponent<chooseDeer>().Choose();
        Destroy(gameObject.transform.GetChild(0).gameObject);
        StartConvo();
        skillUI.ClearLevel(6);
    }

    private void Animation()
    {
        Debug.Log(navMeshAgent.speed);
    }
}
