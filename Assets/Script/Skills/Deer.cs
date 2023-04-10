using UnityEngine;

public class Deer : MonoBehaviour
{
    public static bool deerActive = false;
    private GameObject[] waters;
    private Animator anim;
    public static float speed;
    public int getSkill;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        waters = GameObject.FindGameObjectsWithTag("Water");
        getSkill = SwitchSkills.getSkill;
    }

    private void Update()
    {
        if (gameObject.transform.childCount >= 1)
        {
            deerActive = gameObject.transform.GetChild(0).gameObject.activeSelf;
            if (!anim)
            {
                anim = GetComponentInChildren<Animator>();
            }
        }

        // 水加上collider
        foreach (GameObject water in waters)
        {
            water.GetComponent<MeshCollider>().enabled = deerActive;
        }

        // 鹿行走、跑步動畫
        if (anim)
            anim.SetFloat("Speed", speed);

        if (deerActive)
        {
            SwitchSkills.getSkill = 0;
        }
        else
        {
            SwitchSkills.getSkill = getSkill;
        }
    }

    public static void ChangeDeerSpeed(float s)
    {
        speed = s;
    }
}
