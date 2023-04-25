using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int baseDamage = 3;
    public GameObject particlePrefab;

    private Rigidbody bulletRigidbody;
    private PlayerStats playerStats;
    private AudioSource audioSource;
    private bool isCollide = false;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        audioSource = GetComponent<AudioSource>();

        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
        audioSource.Play();
    }

    private void Update()
    {
        if (isCollide)
        {
            if (!audioSource.isPlaying)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(baseDamage + playerStats.damage.GetValue());
        }
        else if (other.GetComponent<Collider>().CompareTag("HiddingObject"))
        {
            other.GetComponent<craneObject>().hurt();
        }

        if (!other.GetComponent<Collider>().CompareTag("Player"))
        {
            Debug.Log("子彈碰到" + other + " destroy");
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            isCollide = true;
        }
    }
}
