using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    #region Singleton
    public static GemManager instance;
    void Awake()
    {
        instance = this;
    }

    #endregion

    List<Item> gemsUsed = new List<Item>();

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
