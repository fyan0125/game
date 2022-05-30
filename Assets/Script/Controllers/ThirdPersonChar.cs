using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonChar : MonoBehaviour
{
    public Transform cam;
    public float speed = 6f;
    public float jumpSpeed;
    public float superJumpSpeed;

    private CharacterController controller;
    private float ySpeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private bool rotateOnMOve = true;

    private PlayerStats playerStats;
    private SwitchSkills jumpSkill;
    Collider npcCollider;

    public Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        jumpSkill = GetComponent<SwitchSkills>();  //超級跳
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        // controller.SimpleMove(direction * direction.magnitude);

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpSkill.currentSkill == 1)
            {
                ySpeed = superJumpSpeed;
            }
            else
            {
                ySpeed = jumpSpeed;
            }
            anim.SetTrigger("isJumping");
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            if (rotateOnMOve)
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        Vector3 velocity = direction * direction.magnitude;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);

        if (playerStats.currentHealth <= 0) Destroy(gameObject);

        if (Input.GetButtonDown("Talk"))
        {
            TalkToNPC();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Bullet"))
        {
            playerStats.TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
        }
        if (other.GetComponent<Collider>().CompareTag("NPC"))
        {
            npcCollider = other;
        }
    }

    private void TalkToNPC()
    {
        npcCollider.GetComponent<DialogueTrigger>().StartConvo();
    }

    public void SetRotateOnMove(bool newRotateOnMove)
    {
        rotateOnMOve = newRotateOnMove;
    }
}
