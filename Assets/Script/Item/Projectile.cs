using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
//    public float life; 

//     void Awake() 
//     {
//        Destroy(gameObject, life);
//     }
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int damage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Setup();
    }

    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        //Instantiate explosion
        Destroy(gameObject);
        // if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        // //Check for enemies 
        // Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        // for (int i = 0; i < enemies.Length; i++)
        // {
        //     //Add explosion force (if enemy has a rigidbody)
        //     if (enemies[i].GetComponent<Rigidbody>())
        //         enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
        // }

        // //Add a little delay, just to make sure everything works fine
        Invoke("Delay", 0.15f);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        //Count up collisions
        collisions++;
        

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Player") && explodeOnTouch) Explode();
        Destroy(gameObject);
    }

    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
       rb.useGravity = useGravity;
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
