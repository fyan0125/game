using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    private DataPersistenceManager dataPersistenceManager;
    public GameObject loadingScreen;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        dataPersistenceManager = GameObject
            .Find("DataPersistenceManager")
            .GetComponent<DataPersistenceManager>();
        loadingScreen = GameObject.Find("LoadingScreen");
        slider = loadingScreen.transform.Find("Slider").GetComponent<Slider>();
        loadingScreen.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                Debug.Log("You Completed ALL Levels");
            }
            else
            {
                //Move to next level
                StartCoroutine(LoadAsynchronously(nextSceneLoad));
                // SceneManager.LoadSceneAsync(nextSceneLoad);
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

    IEnumerator LoadAsynchronously(int scenIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
