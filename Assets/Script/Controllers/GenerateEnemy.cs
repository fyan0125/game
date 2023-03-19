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

    void Update()
    {
        
    }

    void EnemyDrop()
    {
        for(int enemyCount = 0; enemyCount < maxEnemyCount; enemyCount++){
            randomIndex = Random.Range(0, theEnemy.Length);
            xPos = Random.Range(-85, 85);
            zPos = Random.Range(-85, 85);
            Instantiate(theEnemy[randomIndex], new Vector3(xPos, 10, zPos), Quaternion.identity);
        }
    }
}
