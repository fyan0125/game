using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class craneObject : CharactorStats
{
    public string sound = "鶴";
    public level5Manager Level5Manager;
    public GameObject floatingTextPrefab;
    PlayerStats playerStats;
    private Animator anim;
    public int getDamageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        anim = player.GetComponentInChildren<Animator>();
        Level5Manager = GameObject.Find("Level5Manager").GetComponent<level5Manager>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (getDamageNum == 1)
        {
            Die();
        }
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Enter");
    //     if (
    //         anim.GetCurrentAnimatorStateInfo(0).IsName("Melee")
    //     )
    //     {
    //         hurt();
    //     }
    // }

    public void hurt(){
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CraneHurt");
        Debug.Log(getDamageNum);
        if (floatingTextPrefab != null)
        {
            ShowFloatingText();
        }
        getDamageNum += 1;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    private void ShowFloatingText()
    {
        var floatText = Instantiate(
            floatingTextPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        Debug.Log("floatingtext");
        floatText.GetComponent<TextMeshPro>().text = sound;
    }

    public override void Die()
    {
        base.Die();
        //Add ragdoll affect / death animation

        //For level 3
        if (Level5Manager.i==0 || Level5Manager.i ==1)
        {
            Level5Manager.MissionComplete();
            Debug.Log(Level5Manager.npcCrane.npcState);
        }
        else{
            NotificationManager.instance.count++;
            NotificationManager.instance.UpdateCount();
            Level5Manager.GameComplete();
        }

        Destroy(gameObject);
    }
}
