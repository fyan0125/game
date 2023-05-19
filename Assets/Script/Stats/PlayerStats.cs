using UnityEngine;
using System.Collections;

public class PlayerStats : CharactorStats, IDataPersistence
{
    private HealthBar healthBar;

    [SerializeField]
    private float hurtFlashTime = 0.2f;
    private Animator hurtOverlayAnim;

    private void Start()
    {
        GemManager.instance.onGemChanged += OnGemChanged;

        hurtOverlayAnim = GameObject.Find("PlayerStatus/HurtOverlay").GetComponent<Animator>();
        healthBar = GameObject.Find("PlayerStatus/Health/HealthBar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage) //受到傷害
    {
        base.TakeDamage(damage);
        StartCoroutine(HurtFlash());
        healthBar.SetHealth(currentHealth);
    }

    public void Regen(int h) //回血
    {
        currentHealth += h;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator HurtFlash()
    {
        hurtOverlayAnim.SetBool("Hurt", true);
        AudioManager.instance.Play("PlayerHurt");
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

            healthBar.SetMaxHealth(maxHealth + health.GetValue());
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public void LoadData(GameData data)
    {
        if (data.currentHealth != 0)
        {
            data.currentHealth = 100;
        }
        this.currentHealth = data.currentHealth;
    }

    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
    }
}
