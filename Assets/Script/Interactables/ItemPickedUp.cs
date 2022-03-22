using UnityEngine;

public class ItemPickedUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        
        PickUp();
    }

    public void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
