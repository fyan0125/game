using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ThirdPersonChar : MonoBehaviour
{
    private Transform cam;

    public float moveSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float normalJumpHeight = 1.2f;
    public float superJumpHeight = 3.6f;
    public float gravity = -15.0f;
    public float jumpTimeout = 0.2f; //Time required to pass before being able to jump again. Set to 0f to instantly jump again
    public float fallTimeout = 0.15f; //Time required to pass before entering the fall state. Useful for walking down stairs

    [Header("Player Grounded")]
    public bool grounded = true;
    public float groundedOffset = 1.15f; //Useful for rough ground

    [Tooltip("Should match the radius of the CharacterController")]
    public float groundedRadius = 0.4f; //The radius of the grounded check.
    public LayerMask groundLayers; //What layers the character uses as ground

    // player
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float jumpHeight = 1.2f;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;
    private bool canDoubleJump = true;

    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    // animation IDs
    private int animIDSpeed;
    private int animIDGrounded;
    private int animIDJump;
    private int animIDFreeFall;
    private int animIDMotionSpeed;

    private CharacterController controller;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private bool rotateOnMove = true;
    private Vector3 SpherePosition; //Check Grouned(with offset)

    private PlayerStats playerStats;
    private SwitchSkills switchSkill;
    private Collider npcCollider;
    private Collider chickenCollider;

    public GameObject hint;
    private GameObject newHint;

    private Animator anim;

    private void Awake()
    {
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        switchSkill = GetComponent<SwitchSkills>();

        AssignAnimationIDs();

        // reset our timeouts on start
        jumpTimeoutDelta = jumpTimeout;
        fallTimeoutDelta = fallTimeout;
    }

    void Update()
    {
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

        JumpAndGravity();
        GroundedCheck();
        Move();

        if (playerStats.currentHealth <= 0)
            Destroy(gameObject);

        if (Input.GetButtonDown("Talk") && !DialogueManager.isTalking)
        {
            TalkToNPC();
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (Input.GetButtonDown("Skill"))
            {
                if (chickenCollider)
                {
                    chickenCollider.GetComponent<chicken>().CatchChicken();
                    anim.SetTrigger("Shock");
                    chickenCollider = null;
                }
            }
        }

        anim.SetBool("Riding", Mount.deerActive);
        anim.SetBool("Flying", Mount.canFly);
    }

    private void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("Speed");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDJump = Animator.StringToHash("Jump");
        animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void GroundedCheck()
    {
        if (Mount.deerActive)
            groundLayers |= (1 << LayerMask.NameToLayer("Water"));
        else
            groundLayers &= ~(1 << LayerMask.NameToLayer("Water"));

        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(
            transform.position.x,
            transform.position.y - groundedOffset,
            transform.position.z
        );
        grounded = Physics.CheckSphere(
            spherePosition,
            groundedRadius,
            groundLayers,
            QueryTriggerInteraction.Ignore
        );
        PlayerSound.soundGrounded = grounded;
        anim.SetBool(animIDGrounded, grounded);
    }

    private void Move()
    {
        float targetSpeed = Input.GetButton("Sprint") ? sprintSpeed : moveSpeed;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude < 0.1f)
            targetSpeed = 0.0f;

        if (inputDirection.magnitude >= 0.1f)
        {
            targetRotation =
                Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetRotation,
                ref turnSmoothVelocity,
                turnSmoothTime
            );
            if (rotateOnMove)
            {
                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }
        }
        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

        if (Mount.canFly)
        {
            Vector3 forward = cam.TransformDirection(Vector3.forward).normalized;
            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            targetDirection = forward * vertical + right * horizontal;
        }

        controller.Move(
            targetDirection.normalized * (targetSpeed * Time.deltaTime)
                + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime
        );

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * 10);
        if (animationBlend < 0.01f)
            animationBlend = 0f;

        anim.SetFloat(animIDSpeed, animationBlend);

        if (Mount.deerActive)
        {
            Mount.ChangeMountSpeed(targetSpeed);
        }
    }

    private void JumpAndGravity()
    {
        if (switchSkill.currentSkill == 1)
        {
            jumpHeight = superJumpHeight;
        }
        else
        {
            jumpHeight = normalJumpHeight;
        }

        if (Mount.canFly)
        {
            float hoverSpeed = 5f;
            verticalVelocity = Mathf.Lerp(
                verticalVelocity,
                Input.GetAxisRaw("Hover") * hoverSpeed,
                2f * Time.deltaTime
            );
        }
        else if (grounded)
        {
            // reset the fall timeout timer
            fallTimeoutDelta = fallTimeout;

            // update animator if using character
            anim.SetBool(animIDJump, false);
            anim.SetBool(animIDFreeFall, false);

            // stop our velocity dropping infinitely when grounded
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }

            // Jump
            if (Input.GetButtonDown("Jump") && jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                // update animator if using character
                anim.SetBool(animIDJump, true);

                canDoubleJump = true;
            }

            // jump timeout
            if (jumpTimeoutDelta >= 0.0f)
            {
                jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else if (canDoubleJump)
        {
            // Jump
            if (Input.GetButtonDown("Jump") && jumpTimeoutDelta <= 0.0f)
            {
                canDoubleJump = false;
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                // update animator if using character
                anim.SetBool(animIDJump, true);
            }
        }
        else
        {
            // reset the jump timeout timer
            jumpTimeoutDelta = jumpTimeout;

            // fall timeout
            if (fallTimeoutDelta >= 0.0f)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                anim.SetBool(animIDFreeFall, true);
            }
        }

        if (verticalVelocity < terminalVelocity && !Mount.canFly)
        {
            verticalVelocity += gravity * Time.deltaTime;
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
            Hint(npcCollider, "npc");
        }
        if (other.GetComponent<Collider>().CompareTag("Chicken"))
        {
            chickenCollider = other;
            Hint(chickenCollider, "chicken");
        }
        if (other.GetComponent<Collider>().CompareTag("Deer"))
        {
            Hint(other, "deer");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (
            other.GetComponent<Collider>().CompareTag("NPC")
            || other.GetComponent<Collider>().CompareTag("Chicken")
            || other.GetComponent<Collider>().CompareTag("Deer")
        )
        {
            Destroy(newHint);
        }
    }

    private void TalkToNPC()
    {
        if (npcCollider)
        {
            npcCollider.GetComponent<DialogueTrigger>().StartConvo();
            npcCollider = null;
            Destroy(newHint);
        }
    }

    public void SetRotateOnMove(bool newRotateOnMove)
    {
        rotateOnMove = newRotateOnMove;
    }

    public void MoveToTarget(Vector3 position, Vector3 rotation = default(Vector3))
    {
        controller.enabled = false;
        transform.position = position;
        if (rotation != default(Vector3))
        {
            transform.eulerAngles = rotation;
        }
        controller.enabled = true;
    }

    public void RotateToTarget(Vector3 rotation)
    {
        controller.enabled = false;
        transform.eulerAngles = rotation;
        controller.enabled = true;
    }

    private void Hint(Collider other, string otherName)
    {
        newHint = Instantiate(hint, new Vector3(0, 0, 0), Quaternion.identity);
        newHint.transform.SetParent(other.transform, false);
        newHint.transform.localPosition = new Vector3(0, 2.3f, 0);
        newHint.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "R";
        if (otherName == "chicken")
        {
            newHint.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "技能鍵";
            newHint.transform.localPosition = new Vector3(0, 1, 0);
        }
        else if (otherName == "deer")
        {
            newHint.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "技能鍵";
            newHint.transform.localPosition = new Vector3(0, 2.3f, 0);
        }
    }
}
