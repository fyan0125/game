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
    public static float vertical;
    public static float horizontal;
    public static float rise;
    public static bool grounded = true;

    private void Start()
    {
        GetWater();
    }

    private void Update()
    {
        // 體驗時的按鍵
        // TODO: 繳交時註解掉
        if (Input.GetKeyDown(KeyCode.P))
        {
            deerActive = false;
            canFly = true;
            if (Deer)
            {
                Deer.SetActive(false);
            }

            Yatagarasu.SetActive(!Yatagarasu.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadSceneAsync(0);
        }

        // ---------------------------------------------------------------------

        if (!Deer || !Yatagarasu)
        {
            foreach (Transform child in transform)
            {
                if (child.name.Contains("Deer"))
                {
                    Deer = child.gameObject;
                    deerAnim = Deer.GetComponent<Animator>();
                    deerAnim.SetFloat("IdleAnimation", 1);
                    Deer.transform.position = new Vector3(0, -1.2f, 0);
                    Deer.transform.localPosition = new Vector3(0, -1.2f, 0);
                }
                else
                {
                    Yatagarasu = child.gameObject;
                    flyAnim = Yatagarasu.GetComponent<Animator>();
                    Yatagarasu.transform.localPosition = new Vector3(0, -1.1f, 0);
                }
            }
        }

        deerActive = Deer ? Deer.activeSelf : false;
        canFly = Yatagarasu ? Yatagarasu.activeSelf : false;
        if (deerActive)
        {
            Deer.transform.position = new Vector3(0, -1.2f, 0);
            Deer.transform.localPosition = new Vector3(0, -1.2f, 0);
        }
        if (canFly)
        {
            Yatagarasu.transform.localPosition = new Vector3(0, -1.1f, 0);
        }
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
        {
            deerAnim.SetFloat("Speed", speed);
            deerAnim.SetFloat("IdleAnimation", 1);
        }
        if (flyAnim)
        {
            flyAnim.SetFloat("Speed", speed);
            flyAnim.SetFloat("Vertical", vertical);
            flyAnim.SetFloat("Horizontal", horizontal);
            flyAnim.SetBool("Grounded", grounded);
            flyAnim.SetFloat("Rise", rise);
        }
    }

    public static void GetWater()
    {
        waters = GameObject.FindGameObjectsWithTag("Water");
    }

    public static void ChangeMountSpeed(float s)
    {
        speed = s;
    }

    public static void GetDirectionAndGrounded(float v, float h, bool g)
    {
        vertical = v;
        horizontal = h;
        grounded = g;
    }

    public static void ChangeRise(float r)
    {
        rise = r;
    }
}
