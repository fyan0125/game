using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharactorStats))]
public class ChracterCombat : MonoBehaviour
{
    CharactorStats myStats;

    void Start()
    {
        myStats = GetComponent<CharactorStats>();
    }

    private void Attack (CharactorStats targetStats)
    {
        targetStats.TakeDamage(myStats.damage.GetValue());
    }
}
