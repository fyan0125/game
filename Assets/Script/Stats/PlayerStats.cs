using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactorStats
{
    public HealthBar healthBar;

    void Start()
    {
        GemManager.instance.onGemChanged += OnGemChanged;

        healthBar.SetMaxHealth(maxHealth);
    }

    void OnGemChanged(Gem newItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
    }

    public override void TakeDamage(int damage)//受到傷害
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);//防止防禦大於傷害時補血

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
