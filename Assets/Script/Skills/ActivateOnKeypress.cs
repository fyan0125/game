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

    void LateUpdate()
    {
        if (vcam != null)
        {
            if (switchSkills.currentSkill == 3 && !boosted)
            {
                vcam.Priority += PriorityBoostAmount;
                boosted = true;
            }
            else if (switchSkills.currentSkill != 3 && boosted)
            {
                vcam.Priority -= PriorityBoostAmount;
                boosted = false;
            }
        }
        if (Reticle != null)
            Reticle.SetActive(boosted);
    }
}
