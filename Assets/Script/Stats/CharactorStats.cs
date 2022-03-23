using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }//其他script可讀取但不可修改
    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)//受到傷害
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);//防止防禦大於傷害時補血

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damages.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()//某種死法(會被其他Script覆寫)
    {
        Debug.Log(transform.name + " died.");
    }
}
