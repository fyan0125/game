using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePoint : MonoBehaviour
{
    public int baseDamage = 5;
    PlayerStats playerStats;

    private void Awake()
    {
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(baseDamage + playerStats.damage.GetValue());
        }
    }
}