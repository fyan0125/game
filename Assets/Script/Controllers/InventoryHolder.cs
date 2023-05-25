using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] public InventorySystem inventorySystem;
    public List<InventoryItemData> inventoryItemDatas;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize, inventoryItemDatas);
    }
    // public void LoadData(GameData data)
    // {
    //     for (int i = 0; i < 17; i++)
    //     {
    //         this.inventorySystem.InventorySlots[i].StackSize = data.inventoryStackSize[i];
    //     }
    // }

    // public void SaveData(ref GameData data)
    // {
    //     for (int i = 0; i < 17; i++)
    //     {
    //         data.inventoryStackSize[i] = this.inventorySystem.InventorySlots[i].StackSize;
    //     }
    // }
}
