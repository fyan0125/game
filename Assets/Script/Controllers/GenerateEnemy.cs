using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject[] theEnemy;
    public GameObject portal;
    public int xPos;
    public int zPos;
    public int yPos;
    public int enemyCount;

    public int maxEnemyCount;

    int randomIndex;

    void Start()
    {
        EnemyDrop();
        yPos = Random.Range(7, 10);
    }

    void EnemyDrop()
    {
         for(int enemyCount = 0; enemyCount < maxEnemyCount; enemyCount++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(40, 80);
            zPos = Random.Range(-30, 85);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 12, zPos), Quaternion.identity);
        }
        for(int enemyCount2 = 0; enemyCount2 < maxEnemyCount; enemyCount2++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-85, 40);
            zPos = Random.Range(55, 85);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 12, zPos), Quaternion.identity);
            //Debug.Log("enemy");
        }
        for(int enemyCount3 = 0; enemyCount3 < maxEnemyCount; enemyCount3++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-85, -30);
            zPos = Random.Range(-45, 55);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 15, zPos), Quaternion.identity);
        }
        for(int enemyCount4 = 0; enemyCount4 < maxEnemyCount; enemyCount4++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-85, 40);
            zPos = Random.Range(-80, -45);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 20, zPos), Quaternion.identity);
        }
    }
}
