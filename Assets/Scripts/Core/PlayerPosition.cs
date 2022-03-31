using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public GameObject player;
    

    private void Start()
    {
        if(PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("Time to load") == 1)
        {
            float pX = player.transform.position.x;
            float pY = player.transform.position.y;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            player.transform.position = new Vector2(pX, pY);
            PlayerPrefs.SetInt("Time to load", 0);
            PlayerPrefs.Save();
        }
        
    }

    public void PlayerPosSave()
    {
        PlayerPrefs.SetFloat("p_x", player.transform.position.x);
        PlayerPrefs.SetFloat("p_y", player.transform.position.y);        
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void PlayerPosLoad()
    {
        PlayerPrefs.SetInt("Time to load", 1);
        PlayerPrefs.Save();
    }
}
