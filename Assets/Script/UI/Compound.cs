using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound : MonoBehaviour
{
    protected InventorySystem inventorySystem;
    [SerializeField] private InventoryHolder inventoryHolder;

    public InventoryItemData item1;
    public InventoryItemData item2;
    public InventoryItemData item3;

    private void Start()
    {
        inventorySystem = inventoryHolder.InventorySystem;
    }

    public void CompoundButton()
    {
        if (inventorySystem.ContainItem(item1, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(1))
                {
                    inventorySystem.RemoveFromInventory(item1, 1);
                    return;
                }
            }
        }
        Debug.Log("Don't have enough items.");
    }
}
