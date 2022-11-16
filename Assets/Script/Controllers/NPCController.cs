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

    private void Start()
    {
        multiAimConstraint.weight = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lookTrans = other.transform.Find("LookTrans");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lookTrans = null;
        }
    }

    private void LateUpdate()
    {
        if (lookTrans)
        {
            aimTarget.SetPositionAndRotation(lookTrans.position, lookTrans.rotation);
            multiAimConstraint.weight = Mathf.Clamp01(
                multiAimConstraint.weight + aimSpeed * Time.deltaTime
            );
        }
        else
        {
            multiAimConstraint.weight = Mathf.Clamp01(
                multiAimConstraint.weight - aimSpeed * Time.deltaTime
            );
        }

        Debug.DrawLine(
            headTrans.position,
            headTrans.position + 3.5f * headTrans.forward,
            Color.red
        );
    }
}
