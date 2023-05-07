using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    public Sprite Icon;
    public string DisplayName;

    [TextArea(4, 4)]
    public string Description;

    [TextArea(4, 4)]
    public string Formula;
    public List<InventorySlot> craftingElement;
    public bool synthetic;
    public int MaxStackSize;

    private PlayerStats playerStats;
    public static float time = 10;
    public static float timeRemaining;
    public static bool timerIsRunning = false;

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = time;
                timerIsRunning = false;
            }
        }
    }

    public virtual void Use()
    {
        Debug.Log("Use " + DisplayName);
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        switch (DisplayName)
        {
            case ("鯛魚燒"):
                playerStats.Regen(25);
                break;
            case ("章魚燒"):
                AudioManager.instance.StartCoroutine(Effect(playerStats.damage, 5));
                break;
            case ("棉花糖"):
                AudioManager.instance.StartCoroutine(Effect(playerStats.armor, 5));
                break;
            case ("刨冰"):
                AudioManager.instance.StartCoroutine(Effect(playerStats.speed, 2));
                break;
        }
    }

    IEnumerator Effect(Stat s, int count)
    {
        s.AddModifier(count);
        yield return new WaitForSeconds(30);
        s.RemoveModifier(count);
    }
}
