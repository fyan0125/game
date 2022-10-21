using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public GameObject RabbitSkillUI;
    public GameObject RabbitSkillIcon;
    public GameObject WolfSkillUI;
    public GameObject WolfSkillIcon;

    private void Update()
    {
        if (
            RabbitSkillUI
            && RabbitSkillIcon
            && !RabbitSkillIcon.activeSelf
            && SwitchSkills.getSkill >= 1
        )
        {
            RabbitSkillIcon.SetActive(true);
            RabbitSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (WolfSkillUI && WolfSkillIcon && !WolfSkillIcon.activeSelf && SwitchSkills.getSkill >= 2)
        {
            WolfSkillIcon.SetActive(true);
            WolfSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void SkillUITransition(int currentSkill)
    {
        switch (SwitchSkills.getSkill)
        {
            case 1:
                if (currentSkill == 1)
                {
                    LeanTween
                        .scale(RabbitSkillUI, new Vector3(1.0f, 1.0f, 1.0f), 0.5f)
                        .setEase(LeanTweenType.easeOutBounce);
                }
                else if (currentSkill == 0)
                {
                    LeanTween
                        .scale(RabbitSkillUI, new Vector3(0.45f, 0.45f, 0.45f), 0.5f)
                        .setEase(LeanTweenType.easeOutBounce);
                }
                break;
            case 2:
                if (currentSkill == 1)
                {
                    LeanTween
                        .scale(RabbitSkillUI, new Vector3(1.0f, 1.0f, 1.0f), 0.5f)
                        .setEase(LeanTweenType.easeOutBounce);
                    LeanTween
                        .scale(WolfSkillUI, new Vector3(0.45f, 0.45f, 0.45f), 0.5f)
                        .setEase(LeanTweenType.easeOutBounce);
                }
                else if (currentSkill == 2)
                {
                    LeanTween
                        .scale(RabbitSkillUI, new Vector3(0.45f, 0.45f, 0.45f), 0.5f)
                        .setEase(LeanTweenType.easeOutBounce);
                    LeanTween
                        .scale(WolfSkillUI, new Vector3(1.0f, 1.0f, 1.0f), 0.5f)
                        .setEase(LeanTweenType.easeOutBounce);
                }
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
