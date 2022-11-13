using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipe : MonoBehaviour
{
    //private static CompoundItems inventort;
    public GameObject compoundItems;
    public GameObject[] inventorySlot = new GameObject[8];
    public InventoryDisplay inventoryDisplay;
    public InventorySlot_UI inventorySlot_UI;
    public Text stackSize1, stackSize2;
    // Start is called before the first frame update
    void Awake()
    {
        compoundItems = GameObject.Find("CompoundItems");
        inventoryDisplay = compoundItems.GetComponent<InventoryDisplay>();
        for(int i = 0; i<8; i++){
            inventorySlot[i] = compoundItems.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Text RefreshAmount(InventoryItemData item)
    {
        
       if(item.DisplayName == "棉花糖"){
        inventorySlot_UI = inventorySlot[1].GetComponent<InventorySlot_UI>();
        stackSize1.text = inventorySlot_UI.itemCount.text;
       }
       return(stackSize1);
    }
}
