using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScreenLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider slider;
    PlayerPosition playerPosData;

    private void Start()
    {
        playerPosData = FindObjectOfType<PlayerPosition>();
    }

    public void NewGame(int sceneIndex)//Load Level
    {
        PlayerPrefs.DeleteKey("p_x");
        PlayerPrefs.DeleteKey("p_y");
        PlayerPrefs.DeleteKey("Time to load");
        PlayerPrefs.DeleteKey("Saved");
        StartCoroutine(LoadAsynchronomusly(sceneIndex));
    }

    public void LoadGame(int sceneIndex)//Load Level
    {
        
        if (PlayerPrefs.GetInt("Load save") == 1)
        {
            StartCoroutine(LoadAsynchronomusly(sceneIndex));
            SceneManager.LoadScene(PlayerPrefs.GetInt("Saved game"));
            
        }
        else
        {
            return;
        }
        
    }

    public void QuitbackMenu(int sceneIndex)//Load Level
    {        
        StartCoroutine(LoadAsynchronomusly(sceneIndex));
    }

    IEnumerator LoadAsynchronomusly (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);          

            slider.value = progress;            
            yield return null;
        }
    }


    public void QuitGame()
    {
        playerPosData.PlayerPosSave();
        PlayerPrefs.SetInt("Load save", 1);
        PlayerPrefs.SetInt("Saved game", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }
    public void QuitEndScene()
    {        
        Application.Quit();
    }
}
