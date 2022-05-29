using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size, List<InventoryItemData> itemData)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot(itemData[i], 0));
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public void RemoveFromInventory(InventoryItemData itemRoRemove, int amountToRemove)
    {
        if (ContainItem(itemRoRemove, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.StackSize > 1)
                {
                    slot.RemoveFromStack(amountToRemove);
                    OnInventorySlotChanged?.Invoke(slot);
                }
                else
                {
                    slot.ClearSlot();
                    OnInventorySlotChanged?.Invoke(slot);
                }
            }
        }
    }

    public bool ContainItem(InventoryItemData itemToAdd, out List<InventorySlot> invSLot)
    {
        invSLot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSLot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
