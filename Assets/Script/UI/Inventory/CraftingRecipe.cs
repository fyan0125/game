using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipe : MonoBehaviour
{
    //更新素材數量
    public int[] inventoryStackSize = new int[8];
    public InventorySlot[] inventoryItem = new InventorySlot[13];
    public GameObject player;
    public InventoryHolder inventoryHolder;
    public InventoryItemData itemData;

    //傳資料給製作、使用But
    private static Compound compound;
    private GameObject compoundBut;

    private static CompoundArea compoundArea;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        inventoryHolder = player.GetComponent<InventoryHolder>();
        compoundArea = GetComponent<CompoundArea>();
        compoundBut = GameObject.Find("CompoundButton");
        compound = compoundBut.GetComponent<Compound>();
    }
    void Update()
    {
        for(int i = 0; i<8; i++){
           inventoryStackSize[i] = inventoryHolder.InventorySystem.InventorySlots[i].StackSize;
        }
        for(int i = 0; i<13 ;i++){
           inventoryItem[i] = inventoryHolder.InventorySystem.InventorySlots[i];
        }
        getItem(itemData);
    }

    public void getItem(InventoryItemData item)
    {
        itemData = item;
        switch(item.DisplayName){
            case("鯛魚燒"):
                RefreshAmount(8, 0, 1);
                break;
            case("蘋果糖"):
                RefreshAmount(9, 3, 2);
                break;
            case("章魚燒"):
                RefreshAmount(10, 5, 2);
                break;
            case("棉花糖"):
                RefreshAmount(11, 4, 2);;
                break;
            case("刨冰"):
                RefreshAmount(12, 6, 7);
                break;
        }
    }

    public void RefreshAmount(int a, int b, int c)
    {
        compoundArea.stackSize1.text = inventoryStackSize[b].ToString();
        compoundArea.stackSize2.text = inventoryStackSize[c].ToString();
        changeTextColor(inventoryStackSize[b], inventoryStackSize[c]);
        compound.updateItem(inventoryItem[a],inventoryItem[b],inventoryItem[c]);
    }
    

    public void changeTextColor(int item1, int item2)
    {
        if(item1 == 0 && item2 == 0){
            compoundArea.stackSize1.color = Color.red;
            compoundArea.stackSize2.color = Color.red;
        }
        else if(item1 > 0 && item2 == 0)
        {
            compoundArea.stackSize2.color = Color.red;
        }
        else if(item1 == 0 && item2 > 0)
        {
            compoundArea.stackSize1.color = Color.red;
        }
        else if(item1 > 0 && item2 > 0){
            compoundArea.stackSize1.color = Color.white;
            compoundArea.stackSize2.color = Color.white;
        }
    }
}
