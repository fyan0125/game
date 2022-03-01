using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem", menuName = "Gem")]
public class Gem : Item
{
    public int armorModifier;
    public int damageModifier;
    public int healthModifier;

    public override void Use()
    {
        base.Use();
        GemManager.instance.UseGem(this);
        RemoveFromInventory();
    }
}
