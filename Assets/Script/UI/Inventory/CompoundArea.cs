using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompoundArea : MonoBehaviour
{
    public Text displayName, intro, formula;
    public Image image;
    public Button compoundButton;

    private static CompoundArea instance;

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
        
        if (item.Formula == "")
        {
            instance.compoundButton.interactable = false;
            instance.formula.text = "-  僅能透過任務取得 -";
        }
        else
        {
            instance.compoundButton.interactable = true;
            instance.formula.text = item.Formula;
        }
    }
}
