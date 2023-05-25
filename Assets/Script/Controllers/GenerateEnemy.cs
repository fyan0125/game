using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject[] theEnemy;
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
        //神社前面的地
         for(int enemyCount = 0; enemyCount < maxEnemyCount; enemyCount++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(33, -60);
            zPos = Random.Range(4, 34);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 2, zPos), Quaternion.identity).transform.parent = GameObject.Find("Mobs").transform;
        }
        //面對神社右邊的地
        for(int enemyCount2 = 0; enemyCount2 < maxEnemyCount; enemyCount2++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-97, -146);
            zPos = Random.Range(-57, -27);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 9 , zPos), Quaternion.identity).transform.parent = GameObject.Find("Mobs").transform;
        }
        //神社後面的地
        for(int enemyCount3 = 0; enemyCount3 < maxEnemyCount; enemyCount3++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-45, 29);
            zPos = Random.Range(-75, -101);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 10, zPos), Quaternion.identity).transform.parent = GameObject.Find("Mobs").transform;
        }
        //神社後面的地
        for(int enemyCount6 = 0; enemyCount6 < maxEnemyCount; enemyCount6++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-13, -97);
            zPos = Random.Range(-121, -101);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 12, zPos), Quaternion.identity).transform.parent = GameObject.Find("Mobs").transform;
        }
        //面對神社左邊的地
        for(int enemyCount4 = 0; enemyCount4 < maxEnemyCount; enemyCount4++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(113, 80);
            zPos = Random.Range(-42, 9);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 8, zPos), Quaternion.identity).transform.parent = GameObject.Find("Mobs").transform;
        }
        //面對神社左邊的地
        for(int enemyCount5 = 0; enemyCount5 < maxEnemyCount; enemyCount5++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(11, 34);
            zPos = Random.Range(-14, -64);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 2, zPos), Quaternion.identity).transform.parent = GameObject.Find("Mobs").transform;
        }
    }
}
