using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound : MonoBehaviour
{
    protected InventorySystem inventorySystem;

    [SerializeField]
    private InventoryHolder inventoryHolder;

    private GameObject compoundArea;
    private CraftingRecipe craftingRecipe;

    public InventorySlot[] inventoryItem = new InventorySlot[3];

    private void Start()
    {
        inventorySystem = inventoryHolder.InventorySystem;
        compoundArea = GameObject.Find("CompoundArea");
        craftingRecipe = compoundArea.GetComponent<CraftingRecipe>();
    }

    public void updateItem(InventorySlot item1, InventorySlot item2 = null, InventorySlot item3 = null)
    {
        inventoryItem[0] = item1;
        inventoryItem[1] = item2 != null ? item2 : null;
        inventoryItem[2] = item3 != null ? item3 : null;
    }

    public void CompoundButton()
    {
        if (inventoryItem[1].StackSize > 0 && inventoryItem[2].StackSize > 0)
        {
            inventorySystem.AddToInventory(inventoryItem[0].ItemData, 1);
            inventorySystem.RemoveFromInventory(inventoryItem[1].ItemData, 1);
            inventorySystem.RemoveFromInventory(inventoryItem[2].ItemData, 1);
            return;
        }
        else
        {
            Debug.Log("Don't have enough items.");
        }
    }

    public void UseButton()
    {
        if(inventoryItem[0].StackSize > 0)
        {
            inventoryItem[0].ItemData.Use();
            inventorySystem.RemoveFromInventory(inventoryItem[0].ItemData, 1);
        }
    }
}
