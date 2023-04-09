using UnityEngine;

public class Deer : MonoBehaviour
{
    public static bool deerActive = false;
    private GameObject[] waters;
    private Animator anim;
    public static float speed;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        waters = GameObject.FindGameObjectsWithTag("Water");
    }

    private void Update()
    {
        if (gameObject.transform.childCount >= 1)
        {
            deerActive = gameObject.transform.GetChild(0).gameObject.activeSelf;
        }
        foreach (GameObject water in waters)
        {
            water.GetComponent<MeshCollider>().enabled = deerActive;
        }
        if (anim)
            anim.SetFloat("Speed", speed);
    }

    public static void ChangeDeerSpeed(float s)
    {
        speed = s;
    }
}
