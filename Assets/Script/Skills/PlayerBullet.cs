using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public int baseDamage = 3;
    PlayerStats playerStats;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void Start()
    {
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(baseDamage + playerStats.damage.GetValue());
        }
        Destroy(gameObject);
    }
}
