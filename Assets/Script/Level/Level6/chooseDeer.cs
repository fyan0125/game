using UnityEngine;
using UnityEngine.AI;

public class chooseDeer : MonoBehaviour
{
    private level6Manager level6Manager;
    public GameObject deerPrefab;

    private Animator anim;
    private float idleAnimation;
    private float nowAnim;
    private int idleTime = 2;
    private NavMeshAgent agent;
    public float range; //radius of sphere
    public Transform centrePoint; //centre of the area the agent wants to move around in

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        level6Manager = GameObject.Find("Level6Manager").GetComponent<level6Manager>();
        SetNextDestination();
        anim.SetFloat("IdleAnimation", 1);
    }

    private void Update()
    {
        if (agent.velocity.magnitude == 0)
        {
            Invoke("SetNextDestination", idleTime);
            nowAnim = Mathf.Lerp(nowAnim, idleAnimation, 0.2f);
            anim.SetFloat("IdleAnimation", nowAnim);
        }
        else
        {
            nowAnim = Mathf.Lerp(nowAnim, 1, 0.2f);
            anim.SetFloat("IdleAnimation", nowAnim);
        }
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnTriggerStay(Collider other)
    {
        if (
            level6Manager.npcState == 4
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
        GameObject deer = Instantiate(
            deerPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity,
            GameObject.Find("Player/Deer").transform
        );
        deer.transform.localPosition = GameObject
            .Find("Player")
            .transform.GetChild(0)
            .transform.position;
        deer.transform.rotation = GameObject
            .Find("Player")
            .transform.GetChild(0)
            .transform.rotation;
        Destroy(gameObject);
    }

    private void SetNextDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                agent.SetDestination(point);
                idleAnimation = Random.Range(0, 3);
                idleTime = Random.Range(3, 6);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
