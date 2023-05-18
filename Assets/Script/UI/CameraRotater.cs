using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    public float speed;
    public float skyboxRotate = 2f;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotate);
    }
}
