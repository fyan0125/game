using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerSkillController : MonoBehaviour
{
    [HideInInspector]
    public GameObject meleeWeapon;

    [HideInInspector]
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

    private SkinnedMeshRenderer[] allMesh;
    private List<Material> playerMaterial = new List<Material>();
    public Texture ghostTexture;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        thirdPersonChar = GetComponent<ThirdPersonChar>();
        switchSkills = GetComponent<SwitchSkills>();

        allMesh = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer mesh in allMesh)
        {
            playerMaterial.AddRange(mesh.materials);
        }
        meleeWeapon = GameObject.Find("狼牙棒");
        shootWeapon = GameObject.Find("大砲");
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

        foreach (Material m in playerMaterial)
        {
            if (switchSkills.currentSkill == 4)
            {
                m.SetFloat("_Mode", 3); // Transparent
                m.mainTexture = ghostTexture;
                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                m.SetInt("_ZWrite", 0);
                // m.DisableKeyword("_ALPHATEST_ON"); // 正常陰影
                m.EnableKeyword("_ALPHABLEND_ON"); // 沒有任何陰影
                m.DisableKeyword("_ALPHAPREMULTIPLY_ON"); // 透明時的陰影
                // m.renderQueue = 3000; // 透出原本的顏色
            }
            else
            {
                m.SetFloat("_Mode", 0); // Opaque
                m.mainTexture = null;
                m.SetInt("_ZWrite", 1);
            }
        }

        meleeWeapon.SetActive((switchSkills.currentSkill == 2));
        shootWeapon.SetActive((switchSkills.currentSkill == 3));
        debugTransform.SetActive((switchSkills.currentSkill == 3));
        thirdPersonChar.SetRotateOnMove(!(switchSkills.currentSkill == 3));
        MobController.AttackPlayer(!(switchSkills.currentSkill == 4));
    }
}
