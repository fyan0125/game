using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcReward : MonoBehaviour
{
    [SerializeField] private Transform reward;
    [SerializeField] private Transform spawnReward;

    Quaternion rotation;

    public void GetReward()
    {
        Instantiate(reward, spawnReward.position, Random.rotation);
    }
}
