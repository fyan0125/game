using UnityEngine;
using UnityEngine.SceneManagement;

public class Mount : MonoBehaviour
{
    public GameObject Deer;
    private Animator deerAnim;
    public static bool deerActive = false;

    public GameObject Yatagarasu;
    private Animator flyAnim;
    public static bool canFly = false;

    private static GameObject[] waters;
    public static float speed;

    private void Start()
    {
        GetWater();
    }

    private void Update()
    {
        if (!Deer || !Yatagarasu)
        {
            foreach (Transform child in transform)
            {
                if (child.name.Contains("Deer"))
                {
                    Deer = child.gameObject;
                    deerAnim = Deer.GetComponent<Animator>();
                    deerAnim.SetFloat("IdleAnimation", 1);
                    Deer.transform.localPosition = new Vector3(0, -1.2f, 0);
                }
                else
                {
                    Yatagarasu = child.gameObject;
                    flyAnim = Yatagarasu.GetComponent<Animator>();
                    Yatagarasu.transform.localPosition = new Vector3(0, -1.2f, 0);
                }
            }
        }

        deerActive = Deer ? Deer.activeSelf : false;
        canFly = Yatagarasu ? Yatagarasu.activeSelf : false;
        if (!SwitchSkills.lockSkill)
        {
            SwitchSkills.lockByMount = deerActive || canFly;
        }

        // 水加上collider
        if (waters.Length >= 0)
        {
            foreach (GameObject water in waters)
            {
                water.GetComponent<MeshCollider>().enabled = deerActive;
            }
        }

        // 坐騎動畫
        if (deerAnim)
            deerAnim.SetFloat("Speed", speed);
        if (flyAnim)
            flyAnim.SetFloat("Speed", speed);
    }

    public static void GetWater()
    {
        waters = GameObject.FindGameObjectsWithTag("Water");
    }

    public static void ChangeMountSpeed(float s)
    {
        speed = s;
    }
}
