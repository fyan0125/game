using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactorStats
{
    public HealthBar healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        GemManager.instance.onGemChanged += OnGemChanged;
    }

    public override void TakeDamage(int damage)//受到傷害
    {
        base.TakeDamage(damage);
        healthBar.SetHealth(currentHealth);
    }

    public void OnGemChanged(Gem newItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            speed.AddModifier(newItem.speedModifier);
            health.AddModifier(newItem.healthModifier);
        }
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
