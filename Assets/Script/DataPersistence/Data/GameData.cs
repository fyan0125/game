using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //來源
    public InventoryHolder inventoryHolder;

    //需儲存之資料
    public int[] inventoryStackSize = new int[17];
    public int currentHealth = 1000; //玩家血量

    //定義在這的會是Default Value
    //當沒有data可以load，遊戲開始時會使用Default Value
    public GameData()
    {
        for (int i = 0; i < 7; i++)
        {
            this.inventoryStackSize[i] = 0;
        }
    }
}
