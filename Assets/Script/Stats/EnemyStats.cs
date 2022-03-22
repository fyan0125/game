using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharactorStats
{
    public override void Die() 
    {
        base.Die();
        //Add ragdoll affect / death animation

        Destroy(gameObject);
    }
}
