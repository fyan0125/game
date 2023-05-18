using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompoundArea : MonoBehaviour
{
    public TextMeshProUGUI displayName, intro, stackSize1, stackSize2, stack, formula;
    public int size1, size2;
    public Image image, craftingElement1, craftingElement2;
    //public List<InventorySlot> craftingElement;
    public Button compoundButton;

    private static CompoundArea instance;
    private static CraftingRecipe crafting;
    private static InventoryItemData itemData;
    private static GameObject Template;
    private static GameObject craftingElement;

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
        Template = GameObject.Find("Template");
        craftingElement = GameObject.Find("craftingElement");
        Template.SetActive(false);
    }
    void Update()
    {
    }

    public static void ChangeProp(InventoryItemData item)
    {
        Template.SetActive(true);
        if(item.DisplayName == "攻擊寶石"||
            item.DisplayName == "護盾寶石"||
            item.DisplayName == "血量寶石"||
            item.DisplayName == "速度寶石"
        )
        {
            craftingElement.SetActive(false);
        }
        else
        {
            craftingElement.SetActive(true);
        }
        itemData = item;
        instance.image.sprite = item.Icon;
        instance.displayName.text = item.DisplayName;
        instance.intro.text = item.Description;
        crafting.getItem(item);
        
        if (item.Formula == "")
        {
            instance.compoundButton.interactable = false;
            instance.formula.text = "僅能透過任務取得";
            instance.craftingElement1.sprite=null;
            instance.craftingElement2.sprite=null;
        }
        else
        {
            instance.compoundButton.interactable = true;
            instance.formula.text = item.Formula;
            instance.craftingElement1.sprite = item.craftingElement[0].ItemData.Icon;
            instance.craftingElement2.sprite = item.craftingElement[1].ItemData.Icon;
        }
    }
}
