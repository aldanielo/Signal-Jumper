using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void RestartGame()
    {
        /*
         * // Assuming you have a PlayerPrefs or a static variable to store the current level
        PlayerPrefs.SetString("CurrentLevel", "Level 1");
        SceneManager.LoadSceneAsync("Level 1");
        */

        
        //SceneManager.LoadSceneAsync("Level 1");
        
        SceneManager.LoadScene(1);
        //GameManager.instance.RestartGame();
    }

    public void ContinueGame()
    {
        string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level 1");
        SceneManager.LoadSceneAsync(currentLevel);

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetString("CurrentLevel");
            SceneManager.LoadSceneAsync(currentLevel);
        }
        else
        {
            // Handle the case where the player hasn't played the game before
            SceneManager.LoadSceneAsync("Level 1");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
