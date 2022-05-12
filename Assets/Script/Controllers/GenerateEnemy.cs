using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject portal;
    public int xPos;
    public int zPos;
    public int enemyCount;

    public int maxEnemyCount;

    

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop(){
        while(enemyCount < maxEnemyCount){
            xPos = Random.Range(-9, 4);
            zPos = Random.Range(-11, -2);
            Instantiate(theEnemy, new Vector3(xPos, -4, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1; 
        }
        if(enemyCount == 0){
            portal.SetActive(true);
        }
    }

}
