using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    public Sprite Icon;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    [TextArea(4, 4)]
    public string Formula;
    public List<InventorySlot> craftingElement;
    public bool synthetic;
    public int MaxStackSize;

    public virtual void Use()
    {
        Debug.Log("Clicked " + DisplayName);
    }
}