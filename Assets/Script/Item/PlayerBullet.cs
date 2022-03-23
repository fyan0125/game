using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public int damage = 5;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 100f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyStats>() != null)
        {
            //hit enemy
            other.GetComponent<EnemyStats>().TakeDamage(damage);
        }
        else
        {
            //hit something else
            Debug.Log("hit something else!");
        }
        Destroy(gameObject);
    }
}
