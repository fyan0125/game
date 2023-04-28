using UnityEngine;
using System.Collections;

public class PlayerStats : CharactorStats
{
    private HealthBar healthBar;

    [SerializeField]
    private float hurtFlashTime = 0.2f;
    private Animator hurtOverlayAnim;

    private AudioManager audioManager;

    private void Start()
    {
        GemManager.instance.onGemChanged += OnGemChanged;

        hurtOverlayAnim = GameObject.Find("PlayerStatus/HurtOverlay").GetComponent<Animator>();
        healthBar = GameObject.Find("PlayerStatus/Health/HealthBar").GetComponent<HealthBar>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage) //受到傷害
    {
        base.TakeDamage(damage);
        StartCoroutine(HurtFlash());
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator HurtFlash()
    {
        hurtOverlayAnim.SetBool("Hurt", true);
        audioManager.Play("PlayerHurt");
        yield return new WaitForSeconds(hurtFlashTime);
        hurtOverlayAnim.SetBool("Hurt", false);
    }

    public void OnGemChanged(Gem newItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            speed.AddModifier(newItem.speedModifier);
            health.AddModifier(newItem.healthModifier);
        }
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
