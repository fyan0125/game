using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactorStats
{
    void Start()
    {
        GemManager.instance.onGemChanged += OnGemChanged;
    }

    void OnGemChanged(Gem newItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
    }

    public override void Die() 
    {
        base.Die();
        
        Destroy(gameObject);
    }

  
}
