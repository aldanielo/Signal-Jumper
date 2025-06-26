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
    public Button playButton;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float scoreMultiplier = 1;
    private float scoreMultiplierTimer = 0f;

    private const string SCORE_HISTORY_KEY = "ScoreHistory";
    private const string LAST_SCORE_KEY = "LastScore";
    private const string LAST_LEVEL_KEY = "LastLevel";

    public static bool isRestarted = false;




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
        score = GetLastScore(); // Load the last score
        scoreText.text = "Score: " + score.ToString();

    }

    public void Start()
    {
        if (!isRestarted)
        {
            Time.timeScale = 0f;
            playButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);

       

            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("Theme");
                AudioManager.instance.Stop("Background");
            }
        }
        else
        {
            // Game was restarted, skip play screen
            Time.timeScale = 1f;
            playButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);


            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("Backgrpund");
                AudioManager.instance.Stop("Theme");
            }

            isRestarted = false; // Reset flag
        }
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

    public void StartGame()
    {
        Time.timeScale = 1f; // Resume game time
        playButton.gameObject.SetActive(false); // Hide Play button

        // switch music again
        /*if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Theme");
            AudioManager.instance.Play("Background");
        }*/

        Time.timeScale = 1f; // Resume game time
        playButton.gameObject.SetActive(false); // Hide Play button
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Theme");
            AudioManager.instance.Play("Background");
        }

    }

    public void AddScore()
    {
        score += (int)scoreMultiplier;
        //score += 1;
        scoreText.text = "Score: " + score.ToString();
        SaveScore(score); // Save after adding score
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        /*
        SaveScore(score);
        // Stop the game
        Time.timeScale = 0f;

        // Optionally, reload the scene or show a game over screen
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Over!");

        AudioManager.instance.Play("Game Over");

        restartButton.gameObject.SetActive(true); // Show the restart button

        */
        SaveScore(score); // Stop the game
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
        AudioManager.instance.Play("Game Over");
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Background");
            AudioManager.instance.Play("Theme");
        }
        restartButton.gameObject.SetActive(true); // Show the restart button

    }

    public void RestartGame()
    {
        /*
        isRestarted = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // AudioManager.instance.Stop("Background"); */

        isRestarted = true;
        Time.timeScale = 1f;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Theme");
            AudioManager.instance.Play("Background");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void RemoveScore()
    {
        score --; // Decrease score
        scoreText.text = "Score: " + score.ToString(); // Update score text
        SaveScore(score); // Save after removing score

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

    // Activates blast function
    public void DeductScore(int points)
    {
        score -= points;
        if (score < 0)
        {
            GameOver();
        }
        scoreText.text = "Score: " + score.ToString();
        SaveScore(score); // Save after deduction
    }

    // Save Last Memory
    public void SaveScore(int score)
    {
        // Save score history
        string scoreHistory = PlayerPrefs.GetString(SCORE_HISTORY_KEY, "");
        scoreHistory += score + ",";
        PlayerPrefs.SetString(SCORE_HISTORY_KEY, scoreHistory);

        // Save last score
        PlayerPrefs.SetInt(LAST_SCORE_KEY, score);
        PlayerPrefs.Save(); // Force saving to disk
        Debug.Log("Score loaded: " + score);

    }

    public void SaveLastLevel(int level)
    {
        PlayerPrefs.SetInt(LAST_LEVEL_KEY, level);
    }

    public string GetScoreHistory()
    {
        return PlayerPrefs.GetString(SCORE_HISTORY_KEY, "");
        //Debug.Log(PlayerPrefs.GetString(SCORE_HISTORY_KEY, ""));
    }

    public int GetLastScore()
    {
        return PlayerPrefs.GetInt(LAST_SCORE_KEY, 0);
    }

    public int GetLastLevel()
    {
        return PlayerPrefs.GetInt(LAST_LEVEL_KEY, 1); // Default to level 1
    }

}
