using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlatformMover platformTyp;
    public Button restartButton;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float scoreMultiplier = 1;
    private float scoreMultiplierTimer = 0f;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        //restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);

    }

    private void Update()
    {
        if (scoreMultiplierTimer > 0f)
        {
            scoreMultiplierTimer -= Time.deltaTime;
            if (scoreMultiplierTimer <= 0f)
            {
                scoreMultiplier = 1f;
            }
        }
    }



    public void AddScore()
    {
        score += (int)scoreMultiplier;
        //score += 1;
        scoreText.text = "Score: " + score.ToString();
    }


    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }


    public void GameOver()
    {

        // Stop the game
        Time.timeScale = 0f;

        // Optionally, reload the scene or show a game over screen
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Over!");

        restartButton.gameObject.SetActive(true); // Show the restart button
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RemoveScore()
    {
        score --; // Decrease score
        scoreText.text = "Score: " + score.ToString(); // Update score text

        if (score < 0)
        {
            GameOver();
        }
    }


    public void ActivateScoreMultiplier(float multiplier, float duration)
    {
        scoreMultiplier = multiplier;
        scoreMultiplierTimer = duration;
    }


    private void DeactivateScoreMultiplier()
    {
        scoreMultiplier = 1;
    }
}
