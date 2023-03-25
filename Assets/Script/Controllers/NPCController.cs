using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCController : MonoBehaviour
{
    public Transform aimTarget; //瞄准对象，Source Objects
    public Transform lookTrans; //玩家身上的瞄准参考Transform
    public MultiAimConstraint multiAimConstraint; //约束
    public float aimSpeed = 4f; //发现玩家时瞄准速度（调节权重）

    public Transform headTrans; //画线相关
    public Transform player;
    public LayerMask whatIsPlayer;
    public float sightRange;
    public bool playerInSightRange,
        NpcLooking;
    Transform target;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        multiAimConstraint.weight = 0;
        target = player.transform;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (lookTrans && !playerInSightRange)
        {
            aimTarget.SetPositionAndRotation(lookTrans.position, lookTrans.rotation);
            multiAimConstraint.weight = Mathf.Clamp01(
                multiAimConstraint.weight + aimSpeed * Time.deltaTime
            );
            NpcLooking = true;
        }
        else if (playerInSightRange)
        {
            multiAimConstraint.weight = Mathf.Clamp01(
                multiAimConstraint.weight - aimSpeed * Time.deltaTime
            );
            NpcLooking = false;
        }
    }
}
