using UnityEngine;

public class ActivateOnKeypress : MonoBehaviour
{
    public int PriorityBoostAmount = 10;
    public GameObject Reticle;

    Cinemachine.CinemachineVirtualCameraBase vcam;
    bool boosted = false;
    SwitchSkills switchSkills;

    void Start()
    {
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
        GameObject player = GameObject.Find("Player");
        switchSkills = player.GetComponent<SwitchSkills>();
    }

    void Update()
    {
        if (vcam != null)
        {
            if (Input.GetButtonDown("SwitchSkills") && switchSkills.currentSkill == 2 && !boosted)
            {
                vcam.Priority += PriorityBoostAmount;
                boosted = true;
            }
            else if (Input.GetButtonDown("SwitchSkills") && switchSkills.currentSkill != 2 && boosted)
            {
                vcam.Priority -= PriorityBoostAmount;
                boosted = false;
            }
        }
        if (Reticle != null)
            Reticle.SetActive(boosted);
    }
}
