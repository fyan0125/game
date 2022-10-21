using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();

    [SerializeField]
    private GameObject debugTransform;

    [SerializeField]
    private Transform ofBulletProjectile;

    [SerializeField]
    private Transform spawnBulletPosition;
    private bool boosted = false;
    private ThirdPersonChar thirdPersonChar;
    private SwitchSkills switchSkills;

    private void Start()
    {
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

        //控制玩家是否跟著滑鼠游標面向
        if (Input.GetButtonDown("SwitchSkills") && switchSkills.currentSkill == 2)
        {
            boosted = true;
        }
        else if (Input.GetButtonDown("SwitchSkills"))
        {
            boosted = false;
            thirdPersonChar.SetRotateOnMove(true);
        }
        /*-------------------------------------------------*/
        // if (boosted)
        // {
        //     thirdPersonChar.SetRotateOnMove(false);

        //     Vector3 worldAimTarget = mouseWorldPosition;
        //     worldAimTarget.y = transform.position.y;
        //     Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        //     transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        // }

        thirdPersonChar.SetRotateOnMove(false);

        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        /*-------------------------------------------------*/

        if (switchSkills.currentSkill == 3)
        {
            debugTransform.SetActive(true);
            if (Input.GetButtonDown("Skill"))
            {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(
                    ofBulletProjectile,
                    spawnBulletPosition.position,
                    Quaternion.LookRotation(aimDir, Vector3.up)
                );
            }
        }
        else
        {
            debugTransform.SetActive(false);
        }
    }
}
