using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public GameObject meleeWeapon;
    public GameObject shootWeapon;

    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();

    [SerializeField]
    private GameObject debugTransform;

    [SerializeField]
    private Transform ofBulletProjectile;

    [SerializeField]
    private Transform spawnBulletPosition;
    private ThirdPersonChar thirdPersonChar;
    private SwitchSkills switchSkills;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        thirdPersonChar = GetComponent<ThirdPersonChar>();
        switchSkills = GetComponent<SwitchSkills>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.transform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (switchSkills.currentSkill == 2)
        {
            if (Input.GetButtonDown("Skill"))
                anim.SetTrigger("Melee");
        }
        else if (switchSkills.currentSkill == 3)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            shootWeapon.SetActive(true);
            debugTransform.SetActive(true);

            if (Input.GetButtonDown("Skill"))
            {
                anim.SetTrigger("Shoot");
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(
                    ofBulletProjectile,
                    spawnBulletPosition.position,
                    Quaternion.LookRotation(aimDir, Vector3.up)
                );
            }
        }

        meleeWeapon.SetActive((switchSkills.currentSkill == 2));
        shootWeapon.SetActive((switchSkills.currentSkill == 3));
        debugTransform.SetActive((switchSkills.currentSkill == 3));
        thirdPersonChar.SetRotateOnMove(!(switchSkills.currentSkill == 3));
    }
}
