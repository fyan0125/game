using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            // if (slot.StackSize > 0) itemCount.text = slot.StackSize.ToString();
            // else itemCount.text = "";
            itemCount.text = slot.StackSize.ToString();
        }
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        // itemSprite.sprite = null;
        // itemSprite.color = Color.clear;
        // itemCount.text = "0";
    }

    // public void OnUISlotClick()
    // {
    //     this.assignedInventorySlot.ItemData.Use();
    // }

    public void OnCompoundSlotClick()
    {
        CompoundArea.ChangeProp(this.assignedInventorySlot.ItemData);
    }
}
