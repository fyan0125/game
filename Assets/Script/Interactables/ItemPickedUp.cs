using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPickedUp : MonoBehaviour
{
    public Item item;
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
