using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompoundArea : MonoBehaviour
{
    public TextMeshProUGUI displayName, intro, stackSize1, stackSize2, stack;
    public Text formula;
    public int size1, size2;
    public Image image, craftingElement1, craftingElement2;
    //public List<InventorySlot> craftingElement;
    public Button compoundButton;

    private static CompoundArea instance;
    private static CraftingRecipe crafting;
    private static InventoryItemData itemData;

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
        crafting = GetComponent<CraftingRecipe>();
    }
    void Update()
    {
    }

    public static void ChangeProp(InventoryItemData item)
    {
        itemData = item;
        instance.image.sprite = item.Icon;
        instance.displayName.text = item.DisplayName;
        instance.intro.text = item.Description;
        crafting.getItem(item);
        
        if (item.Formula == "")
        {
            instance.compoundButton.interactable = false;
            instance.formula.text = "-  僅能透過任務取得 -";
            instance.craftingElement1.sprite=null;
            instance.craftingElement2.sprite=null;
        }
        else
        {
            instance.compoundButton.interactable = true;
            instance.formula.text = item.Formula;
            instance.craftingElement1.sprite = item.craftingElement[0].ItemData.Icon;
            instance.craftingElement2.sprite = item.craftingElement[1].ItemData.Icon;
            // instance.stackSize1.text = instance.size1.ToString();
            // instance.stackSize2.text = instance.size2.ToString();
        }
    }
}
