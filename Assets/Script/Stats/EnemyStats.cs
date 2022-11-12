using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStats : CharactorStats
{
    public string sound;
    public GameObject floatingTextPrefab;
    public EnemyHealthBar enemyHealthBar;
    public MobController mob;

    private void Start()
    {
        enemyHealthBar.SetMaxHealth(maxHealth);
        mob =  GetComponent<MobController>();
    }

    void Update(){
        //damageSelf();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void damageSelf(){
        if (currentHealth>0){
            currentHealth-=1;
            waiter();
        }
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(100);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        enemyHealthBar.SetHealth(currentHealth);

        if (transform.name.Contains("Lantern"))
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("LanternHurt");
        }
        else if (transform.name.Contains("Umbrella"))
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("UmbrellaHurt");
        }

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
        mob.dropItem();
        
        Destroy(gameObject);
    }
}
