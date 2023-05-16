using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    private DataPersistenceManager dataPersistenceManager;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        dataPersistenceManager =  GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 7) /* < Change this int value to whatever your
                                                                   last level build index is on your
                                                                   build settings */
            {
                Debug.Log("You Completed ALL Levels");

                //Show Win Screen or Somethin.
            }
            else
            {
                //Move to next level
                SceneManager.LoadScene(nextSceneLoad);
                Mount.GetWater();
                dataPersistenceManager.SaveGame();
                //Setting Int for Index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }
    }
}
