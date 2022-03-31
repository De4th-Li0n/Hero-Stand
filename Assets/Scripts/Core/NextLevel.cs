using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private int LoadNextLevel;
    public SoundManager soundManager;
    PlayerPosition playerPosData;


    private void Start()
    {
        playerPosData = FindObjectOfType<PlayerPosition>();
        LoadNextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.DeleteKey("p_x");
        PlayerPrefs.DeleteKey("p_y");
        PlayerPrefs.DeleteKey("Time to load");
        PlayerPrefs.DeleteKey("Saved");
        SceneManager.LoadScene(LoadNextLevel);
        Object.Destroy(soundManager);
    }
}
