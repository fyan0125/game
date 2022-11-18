using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoard : MonoBehaviour
{
    public GameObject Level4UI;
    public GameObject chickenPrefab;
    public int chickenNum;
    public GameObject[] places;

    private void Start()
    {
        Level4UI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player") && Level4UI.activeSelf == false)
        {
            Level4UI.SetActive(true);
        }
    }

    public void GameStart()
    {
        List<int> listNumbers = new List<int>();
        int number;
        for (int i = 0; i < chickenNum; i++)
        {
            do
            {
                number = Random.Range(0, places.Length);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }

        for (int i = 0; i < chickenNum; i++)
        {
            Instantiate(
                chickenPrefab,
                places[listNumbers[i]].transform.position,
                Quaternion.identity
            );
        }
    }
}
