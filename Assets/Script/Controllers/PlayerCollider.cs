using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerStats playerStats;
    Collider npcCollider;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (playerStats.currentHealth <= 0) Destroy(gameObject);

        if (Input.GetButtonDown("Talk"))
        {
            TalkToNPC();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Bullet"))
        {
            playerStats.TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
        }
        if (other.GetComponent<Collider>().CompareTag("NPC"))
        {
            npcCollider = other;
        }
    }

    private void TalkToNPC()
    {
        npcCollider.GetComponent<DialogueTrigger>().StartConvo();
    }
}
