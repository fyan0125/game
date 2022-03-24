using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private GameObject debugTransform;
    [SerializeField] private Transform ofBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    bool boosted = false;
    private ThirdPersonChar thirdPersonChar;
    private SwitchSkiils switchSkiils;

    private void Start()
    {
        thirdPersonChar = GetComponent<ThirdPersonChar>();
        switchSkiils = GetComponent<SwitchSkiils>();
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

        if (Input.GetButtonDown("SwitchSkiils") && switchSkiils.currentSkill == 2)
        {
            boosted = !boosted;
        }

        if (boosted)
        {
            thirdPersonChar.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else thirdPersonChar.SetRotateOnMove(true);

        if (switchSkiils.currentSkill == 2)
        {
            debugTransform.SetActive(true);
            if (Input.GetButtonDown("Skill"))
            {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(ofBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
        }
        else
        {
            debugTransform.SetActive(false);
        }
    }
}