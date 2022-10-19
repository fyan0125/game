using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharactorStats
{
    public override void Die() 
    {
        base.Die();
        //Add ragdoll affect / death animation

        //For level 3
        NotificationManager.instance.count++;
        NotificationManager.instance.UpdateCount();

        Destroy(gameObject);
    }
}
