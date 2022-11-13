using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompoundArea : MonoBehaviour
{
    public Text displayName, intro, formula, stackSize1, stackSize2;
    public Image image, craftingElement1, craftingElement2;
    //public List<InventorySlot> craftingElement;
    public Button compoundButton;

    private static CompoundArea instance;
    private static CraftingRecipe crafting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ChangeProp(InventoryItemData item)
    {
        instance.image.sprite = item.Icon;
        instance.displayName.text = item.DisplayName;
        instance.intro.text = item.Description;
        Text stack = crafting.RefreshAmount(item);
        
        if (item.Formula == "")
        {
            instance.compoundButton.interactable = false;
            instance.formula.text = "-  僅能透過任務取得 -";
        }
        else
        {
            instance.compoundButton.interactable = true;
            instance.formula.text = item.Formula;
            // for (int i=0; i<2; i++){
            //     instance.craftingElement[i] = item.craftingElement[0];
            // }
            instance.craftingElement1.sprite = item.craftingElement[0].ItemData.Icon;
            instance.craftingElement2.sprite = item.craftingElement[1].ItemData.Icon;
            instance.stackSize1.text = item.craftingElement[0].StackSize.ToString();
            instance.stackSize2.text = item.craftingElement[1].StackSize.ToString();
        }
    }
}
