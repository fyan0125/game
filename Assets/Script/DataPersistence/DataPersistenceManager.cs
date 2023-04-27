using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;

    //可以取用但不能更改這裡的值
    public static DataPersistenceManager instance { get; private set;} 

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("在這個場景找到多於一個 Data Persistence Manager");
        }
        instance = this;
    }

    private void Start() 
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // TO-DO - Load any saved data from a file using the data handler
        // if no data can be loaded, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data can be found. Initailize data to default.");
            NewGame();
        }

        // TO-DO - push the Loaded data to all the script that need it. 
    }

    public void SaveGame()
    {
        // TO-DO - pass the data to other scripts so they can update it

        // TO-DO - save that data to a flie using the data handler 
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
