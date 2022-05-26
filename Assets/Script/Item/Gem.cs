using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem", menuName = "InventorySystem/Gem")]
public class Gem : InventoryItemData
{
    public int armorModifier;
    public int damageModifier;
    public int healthModifier;
    public int speedModifier;

    public override void Use()
    {
        base.Use();
        GemManager.instance.UseGem(this);
    }
}