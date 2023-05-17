using UnityEngine;

public class SwitchSkills : MonoBehaviour
{
    public static int getSkill = 0;
    public int currentSkill = 0;
    public static bool lockSkill = false;
    public static bool lockByMount = false;

    GameObject GameManager;
    SkillUI skillUI;

    private Animator anim;

    public void Start()
    {
        GameManager = GameObject.Find("GameManager");
        skillUI = GameManager.GetComponent<SkillUI>();
        anim = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        if (getSkill == 0 || lockSkill || lockByMount)
        {
            currentSkill = 0;
            skillUI.SkillUITransition(currentSkill);
        }
        if (Input.GetButtonDown("SwitchSkills"))
        {
            if (currentSkill < getSkill)
            {
                currentSkill += 1;
            }
            else if (getSkill == 1)
            {
                currentSkill = 0;
            }
            else
                currentSkill = 1;
            anim.SetInteger("currentSkill", currentSkill);
            if (getSkill != 0 && !lockSkill && !lockByMount)
            {
                skillUI.SkillUITransition(currentSkill);
            }
        }
        anim.SetInteger("currentSkill", currentSkill);
    }
}
