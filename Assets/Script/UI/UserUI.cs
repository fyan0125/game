using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/* This object manages the inventory UI. */

public class UserUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject packageUI;
    public GameObject settingUI;
    public Transform itemsParent;

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            packageUI.SetActive(!packageUI.activeSelf);

            UpdateUI();
            if (GameIsPaused) Resume();
            else Pause();
        }
        if (Input.GetButtonDown("Pause"))
        {
            settingUI.SetActive(!settingUI.activeSelf);

            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) slots[i].AddItem(inventory.items[i]);
            else slots[i].ClearSlot();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
