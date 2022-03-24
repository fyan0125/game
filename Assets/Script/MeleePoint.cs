using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePoint : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(damage);
        }
    }
}