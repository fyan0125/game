using UnityEngine;

public class Deer : MonoBehaviour
{
    public static bool deerActive = false;
    private GameObject[] waters;
    private Animator anim;
    public static float speed;
    private int getSkill;

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
            Vector3 deerPosition = gameObject.transform
                .GetChild(0)
                .gameObject.transform.localPosition;
            if (deerPosition != new Vector3(0, -1.2f, 0))
            {
                gameObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(
                    0,
                    -1.2f,
                    0
                );
            }
            if (!anim)
            {
                anim = GetComponentInChildren<Animator>();
                anim.SetFloat("IdleAnimation", 1);
            }
            if (deerActive)
            {
                getSkill = SwitchSkills.getSkill;
                SwitchSkills.getSkill = 0;
            }
            else
            {
                SwitchSkills.getSkill = getSkill;
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
    }

    public static void ChangeDeerSpeed(float s)
    {
        speed = s;
    }
}
