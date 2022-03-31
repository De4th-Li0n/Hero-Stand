using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    PlayerPosition playerPosData;

    public static bool GameisPause = false;

    private void Start()
    {
        playerPosData = FindObjectOfType<PlayerPosition>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {        
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameisPause = true;
    }

    public void Resume()
    {        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameisPause = false;
    }

    public void Home(int sceneID)
    {
        playerPosData.PlayerPosSave();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        PlayerPrefs.SetInt("Load save", 1);
        PlayerPrefs.SetInt("Saved game", SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        playerPosData.PlayerPosSave();
        PlayerPrefs.SetInt("Load save", 1);
        PlayerPrefs.SetInt("Saved game", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }

}
