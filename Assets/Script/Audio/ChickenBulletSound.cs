using UnityEngine;

public class ChickenBulletSound : MonoBehaviour
{
    private void Crow()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
