using UnityEngine;

public class Water : MonoBehaviour
{
    [Tooltip("待多久開始扣血")]
    public float stayTime = 1;

    [Tooltip("多久扣一次血")]
    public float hurtRepeatTime = 1;
    public int damage = 5;
    private float time = 0;
    private float hurtInterval;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        hurtInterval = hurtRepeatTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("PlayerHead"))
        {
            time = 0;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("PlayerHead"))
        {
            time += Time.deltaTime;
            if (time > stayTime)
            {
                hurtInterval -= Time.deltaTime;
                if (hurtInterval <= 0)
                {
                    Hurt();
                }
            }
        }
    }

    private void Hurt()
    {
        hurtInterval = hurtRepeatTime;
        playerStats.TakeDamage(damage);
    }
}
