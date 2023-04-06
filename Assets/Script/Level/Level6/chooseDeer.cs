using UnityEngine;

public class chooseDeer : MonoBehaviour
{
    private level6Manager level6Manager;

    private void Start()
    {
        level6Manager = GameObject.Find("Level6Manager").GetComponent<level6Manager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player") && Input.GetButtonDown("Skill"))
        {
            level6Manager.ShowUI(gameObject);
        }
    }
}
