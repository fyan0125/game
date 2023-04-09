using UnityEngine;

public class chooseDeer : MonoBehaviour
{
    private level6Manager level6Manager;
    public GameObject deerPrefab;

    private void Start()
    {
        level6Manager = GameObject.Find("Level6Manager").GetComponent<level6Manager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (
            level6Manager.npcState <= 4
            && other.GetComponent<Collider>().CompareTag("Player")
            && Input.GetButtonDown("Skill")
        )
        {
            level6Manager.ShowUI(gameObject);
        }
    }

    public void Choose()
    {
        Debug.Log("玩家選擇的鹿: " + gameObject);
        GameObject deer = Instantiate(deerPrefab);
        deer.transform.parent = GameObject.Find("Player/Deer").transform;
        deer.transform.position = GameObject
            .Find("Player")
            .transform.GetChild(0)
            .transform.position;
        deer.transform.rotation = GameObject
            .Find("Player")
            .transform.GetChild(0)
            .transform.rotation;
        Destroy(gameObject);
    }
}
