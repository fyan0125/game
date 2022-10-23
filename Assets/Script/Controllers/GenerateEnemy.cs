using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject[] theEnemy;
    public GameObject portal;
    public int xPos;
    public int zPos;
    public int enemyCount;

    public int maxEnemyCount;

    int randomIndex;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    void Update(){
        randomIndex = Random.Range(0, theEnemy.Length);
    } 

    IEnumerator EnemyDrop(){
        while(enemyCount < maxEnemyCount){
            xPos = Random.Range(-121, 114);
            zPos = Random.Range(-121, 97);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 7, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1; 
        }
    }

}
