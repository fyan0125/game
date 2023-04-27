using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool notOnSlide = true;
    private float groundedRadius; //The radius of the grounded check.
    public float groundedOffset = 1.15f; //Useful for rough ground
    public LayerMask groundLayers; //What layers the character uses as ground

    // player
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float jumpHeight = 1.2f;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;
    private bool canDoubleJump = true;

    public float slideFriction = 0.3f; // ajusting the friction of the slope
    public float slideSpeed = 5;
    private Vector3 hitNormal; //orientation of the slope.

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
        groundedRadius = controller.radius;
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

        notOnSlide = Vector3.Angle(Vector3.up, hitNormal) <= controller.slopeLimit;

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
            targetSpeed *= 1.5f;
            Mount.GetDirectionAndGrounded(targetDirection.y, horizontal, grounded);
            Mount.ChangeMountSpeed(targetSpeed);
        }
        else if (Mount.deerActive)
        {
            targetSpeed *= 1.5f;
            Mount.ChangeMountSpeed(targetSpeed);
        }

        if (!notOnSlide)
        {
            targetDirection.x = ((1f - hitNormal.y) * hitNormal.x) * slideSpeed;
            targetDirection.z = ((1f - hitNormal.y) * hitNormal.z) * slideSpeed;
            controller.Move(
                targetDirection.normalized * (Time.deltaTime)
                    + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime
            );
        }
        else
        {
            controller.Move(
                targetDirection.normalized * (targetSpeed * Time.deltaTime)
                    + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime
            );
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * 10);
        if (animationBlend < 0.01f)
            animationBlend = 0f;

        anim.SetFloat(animIDSpeed, animationBlend);
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
            Mount.ChangeRise(verticalVelocity);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
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
            Hint(npcCollider);
        }
        if (other.GetComponent<Collider>().CompareTag("Chicken"))
        {
            chickenCollider = other;
            Hint(chickenCollider);
        }
        if (other.GetComponent<Collider>().CompareTag("Deer"))
        {
            Hint(other);
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

    private void Hint(Collider other)
    {
        Destroy(newHint);
        newHint = Instantiate(hint, new Vector3(0, 0, 0), Quaternion.identity);
        newHint.transform.SetParent(other.transform, false);

        if (other.GetComponent<Collider>().CompareTag("Chicken"))
        {
            newHint.GetComponent<Hint>().talkHint = false;
            newHint.transform.localPosition = new Vector3(0, 1, 0);
            return;
        }

        if (other.GetComponent<Collider>().CompareTag("Deer"))
        {
            newHint.GetComponent<Hint>().talkHint = false;
            newHint.transform.localPosition = new Vector3(0, 2.3f, 0);
            return;
        }

        if (
            SceneManager.GetActiveScene().buildIndex == 1
            || SceneManager.GetActiveScene().buildIndex == 3
            || SceneManager.GetActiveScene().buildIndex == 4
        )
        {
            newHint.transform.localPosition = new Vector3(0, 1.5f, 0);
            newHint.GetComponent<Hint>().talkHint = true;
        }
        else
        {
            newHint.transform.localPosition = new Vector3(0, 2.5f, 0);
            newHint.GetComponent<Hint>().talkHint = true;
        }
    }
}
