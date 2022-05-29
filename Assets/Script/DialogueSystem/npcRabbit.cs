using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcRabbit : DialogueTrigger
{
    public Conversation convo1;
    public Conversation convo2;

    public Button rabbitbtn;
    public Button memorybtn;
    public GameObject rabbitIcon;
    public GameObject memoryIcon;

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public float sightRange;
    public bool playerInSightRange;
    Transform target;
    public GameObject SendPoint;
    private showPortal sP;

    private void Awake() 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = player.transform;
        sP = SendPoint.GetComponent<showPortal>();
        //SendPoint = GameObject.Find("Send Point");
    }
    private void Update()
    {
        if (npcState == 3 && DialogueManager.EndConversation())
        {
            rabbitbtn.interactable = true;
            rabbitIcon.SetActive(false);
            memorybtn.interactable = true;
            memoryIcon.SetActive(true);
            SwitchSkills.getSkill = 1;
            NpcReward.GetReward();
            npcState++;
            sP.isClear = true;
        }

        //Following Player
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if(!playerInSightRange&& rabbitbtn.interactable) followPlayer();
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
                npcState += 1;
                break;
            default:
                convo = convo1;
                break;
        }
        DialogueManager.StartConversation(convo);
    }

    public void followPlayer()
    {
        agent.speed = 2;
        agent.destination = target.position;
    }
}
