using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    private Vector3 nextScenePosition;
    private UserUI userUI;
    private ThirdPersonChar player;
    private DataPersistenceManager dataPersistenceManager;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        userUI = GameObject.Find("Canvas").GetComponent<UserUI>();
        player = GameObject.Find("Player").GetComponent<ThirdPersonChar>();
    }
    
    public void transferScene(int scene)
    {
        //Move to next level
        SceneManager.LoadScene(scene);
        Mount.GetWater();
        userUI.Resume();
        switch(scene)
        {
            case 1:
                player.MoveToTarget(new Vector3(30, 5, 29),new Vector3(0, 0, 0));
                break;
            case 2:
                player.MoveToTarget(new Vector3(-7, 22, -82),new Vector3(0, 0, 0));
                break;
            case 3:
                player.MoveToTarget(new Vector3(-17, 10, -40),new Vector3(0, 0, 0));
                break;
            case 4:
                player.MoveToTarget(new Vector3(-24, -9, -55),new Vector3(0, -180, 0));
                break;
            case 5:
                player.MoveToTarget(new Vector3(31, 2, 29),new Vector3(0, 0, 0));
                break;
            case 6:
                player.MoveToTarget(new Vector3(-34, 7, 61),new Vector3(0, 0, 0));
                break;
            default:
                break;
        }
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
                //Setting Int for Index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }
    }
}
