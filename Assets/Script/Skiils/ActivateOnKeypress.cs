using UnityEngine;

public class ActivateOnKeypress : MonoBehaviour
{
    public int PriorityBoostAmount = 10;
    public GameObject Reticle;

    Cinemachine.CinemachineVirtualCameraBase vcam;
    bool boosted = false;
    SwitchSkiils switchSkiils;

    void Start()
    {
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
        GameObject player = GameObject.Find("Player");
        switchSkiils = player.GetComponent<SwitchSkiils>();
    }

    void Update()
    {
        if (vcam != null)
        {
            if (Input.GetButtonDown("SwitchSkiils") && switchSkiils.currentSkill == 1 && !boosted)
            {
                vcam.Priority += PriorityBoostAmount;
                boosted = true;
            }
            else if (Input.GetButtonDown("SwitchSkiils") && switchSkiils.currentSkill != 1 && boosted)
            {
                vcam.Priority -= PriorityBoostAmount;
                boosted = false;
            }
        }
        if (Reticle != null)
            Reticle.SetActive(boosted);
    }
}
