using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private LayerMask lm = new LayerMask();
    [SerializeField] private Transform dt;
    bool boosted = false;
    public KeyCode ActivationKey = KeyCode.LeftControl;
    private ThirdPersonChar thirdPersonChar;

    private void Start()
    {
        thirdPersonChar = GetComponent<ThirdPersonChar>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, lm))
        {
            dt.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetButtonDown("Aim"))
        {
            if (!boosted) boosted = !boosted;
            else boosted = !boosted;
        }

        if (boosted)
        {
            thirdPersonChar.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            thirdPersonChar.SetRotateOnMove(true);
        }
    }
}