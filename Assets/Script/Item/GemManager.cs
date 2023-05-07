using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;

    void Awake()
    {
        instance = this;
    }

    List<InventoryItemData> gemsUsed = new List<InventoryItemData>();

    public delegate void OnGemChanged(Gem newItem);
    public OnGemChanged onGemChanged;

    public void UseGem(Gem newItem)
    {
        gemsUsed.Add(newItem);
        if (onGemChanged != null)
        {
            onGemChanged.Invoke(newItem);
        }
    }
}
