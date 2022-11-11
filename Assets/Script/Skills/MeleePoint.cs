using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePoint : MonoBehaviour
{
    public int baseDamage = 5;
    PlayerStats playerStats;

    private Animator anim;

    private void Awake()
    {
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        anim = player.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (
            anim.GetCurrentAnimatorStateInfo(0).IsName("Melee")
            && other.GetComponent<Collider>().CompareTag("Enemy")
        )
        {
            other.GetComponent<EnemyStats>().TakeDamage(baseDamage + playerStats.damage.GetValue());
        }
    }
}
