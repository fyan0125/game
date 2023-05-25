using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public InventoryItemData ItemData;

    [SerializeField]
    private float rotationSpeed;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();
        if (!inventory)
            return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            AudioManager.instance.Play("Eat");
            Destroy(this.gameObject);
        }
    }
}
