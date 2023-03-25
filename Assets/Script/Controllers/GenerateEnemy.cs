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
        for(int enemyCount1 = 0; enemyCount1 < maxEnemyCount; enemyCount++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(30, 80);
            zPos = Random.Range(-85, 37);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 10, zPos), Quaternion.identity);
        }
        for(int enemyCount2 = 0; enemyCount2 < maxEnemyCount; enemyCount++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-85, 30);
            zPos = Random.Range(0, 37);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 10, zPos), Quaternion.identity);
            Debug.Log("enemy");
        }
        for(int enemyCount3 = 0; enemyCount3 < maxEnemyCount; enemyCount++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-85, -44);
            zPos = Random.Range(-85, 0);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 30, zPos), Quaternion.identity);
        }
    }
}
