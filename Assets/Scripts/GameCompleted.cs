using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameCompleted : MonoBehaviour
{
    public GameObject gameCompletePanel;

    //public TextMeshProUGUI scoreText;
    public GameManager gameManager;
    //public GameObject finishBar;

    void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Player"))
        {
            CompleteGame();
        }
    }
    public void CompleteGame()
    {
        gameCompletePanel.SetActive(true);
        //scoreText.text = "Your Score: " + score;
        Time.timeScale = 0; //pause the game
        Debug.Log("Game ended");
        AudioManager.instance.Play("Theme");
        AudioManager.instance.Stop("Background");
    }
}
