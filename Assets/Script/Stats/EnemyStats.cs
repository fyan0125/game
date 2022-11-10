using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStats : CharactorStats
{
    public string sound;
    public GameObject floatingTextPrefab;
    public EnemyHealthBar enemyHealthBar;

    private void Start()
    {
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        enemyHealthBar.SetHealth(currentHealth);

        if (floatingTextPrefab != null)
        {
            ShowFloatingText();
        }
    }

    private void ShowFloatingText()
    {
        var text = Instantiate(
            floatingTextPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        text.GetComponent<TMP_Text>().text = sound;
    }

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
