using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameStartPanel;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float scoreMultiplier = 1;
    private float scoreMultiplierTimer = 0f;

    private const string SCORE_HISTORY_KEY = "ScoreHistory";
    private const string LAST_SCORE_KEY = "LastScore";
    private const string LAST_LEVEL_KEY = "LastLevel";

    public static bool isRestarted = false;

    private bool hasSpawnedBar = false;


    //private string currentLevel;

    //FinishBar Spawn
    public int requiredTime = 60; // Time required to play before the bar spawns
    public int requiredScore = 100; // Score required to reach before the bar spawns
    //public GameObject bar; // The bar game object

    private float startTime; // Time when the level starts
    private float currentTime; // Current time played
    //private int currentScore; // Current score

    //public GameObject gameComletePanel;
    public GameObject gameOverPanel;
    public GameObject player;
    GameCompleted gameCompleted;

    public List<LevelConfig> levelConfigs = new List<LevelConfig>();

    [System.Serializable]
    public class LevelConfig
    {
        public int requiredScore;
        public float requiredTime;
    }


    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //if (instance != this)
        {
            Destroy(gameObject);
        }

        //restartButton.onClick.AddListener(RestartGame);

        if (isRestarted)
        {
            score = 0;
            isRestarted = true;
        }
        else
        {
            score = GetLastScore(); // Load the last score
        }
        
        scoreText.text = "Score: " + score.ToString();
    }

    public void Start()
    {
        gameCompleted = GameObject.FindObjectOfType<GameCompleted>();
        //DontDestroyOnLoad (bar);
        startTime = Time.time;
        //bar.SetActive(false); // Deactivate the bar initially

        if (!isRestarted)
        {
            Time.timeScale = 0f;
            /*playButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);
            */
            gameStartPanel.SetActive(true);

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
            /*
            playButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            */

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
        currentTime = Time.time - startTime;

        //check if the player has reached the require score and time

        //if (score >= requiredScore && currentTime >= requiredTime) // && gameCompleted != null)
        //{
        //  gameCompleted.SpawnBar();
        //}

        if (SceneManager.GetActiveScene().buildIndex < levelConfigs.Count)
        {
            LevelConfig currentConfig = levelConfigs[SceneManager.GetActiveScene().buildIndex];

            currentTime = Time.time - startTime;

            if (!hasSpawnedBar && score >= requiredScore && currentTime >= requiredTime)
            {
                gameCompleted.SpawnBar();
                hasSpawnedBar = true;
            }

            // Multiplier countdown
            if (scoreMultiplierTimer > 0f)
            {
                scoreMultiplierTimer -= Time.deltaTime;
                if (scoreMultiplierTimer <= 0f)
                {
                    scoreMultiplier = 1f;
                }
            }
        }


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
        gameStartPanel.SetActive(false);

        Time.timeScale = 1f; // Resume game time
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Theme");
            AudioManager.instance.Play("Background");
        }

    }

    public void AddScore()
    {
        score += (int)scoreMultiplier;
        //int scoreToAdd = (int)scoreMultiplier;
        //score += scoreToAdd;
        //currentScore = score; // tracks players score 
        Debug.Log(score);
        scoreText.text = "Score: " + score.ToString(); 

        SaveScore(score); // Save after adding score
    }

    public void ResetScore()
    {
        score = 0;
        //currentScore = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    


    public void GameOver()
    {

        SaveScore(score); 
        // Stop the game
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
        AudioManager.instance.Play("Game Over");
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Background");
            AudioManager.instance.Play("Theme");
        }
        gameOverPanel.SetActive(true); // Show the restart button

    }

    public void RestartGame()
    {
        //PlayerPrefs.SetInt("Score", 0);
        //score = 0;
        //scoreText.text = "Score: " + score;


        //bar = Resources.Load<GameObject>("FinishBar");

        isRestarted = true;
        Time.timeScale = 1f;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("Theme");
            AudioManager.instance.Play("Background");
        }
        
        // gameStartPanel.SetActive(false);
        ResetScore();
        startTime = Time.time;

        //AddScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Awake();

    }

    public void RemoveScore(int points)
{
        score--; // Decrease score


        if (score < 0)
        {
            GameOver();
            //gameStartPanel.SetActive(false);

        }

        // Activates blast function
        if (player != null && player.transform.position.y < -12)
        {
            score -= points;
            if (score < 0)
            {
                GameOver();

            }
            //scoreText.text = "Score: " + score.ToString();
            //SaveScore(score); // Save after deduction
        }
        scoreText.text = "Score: " + score.ToString(); // Update score text
        SaveScore(score); // Save after removing score

        
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
            //gameStartPanel.SetActive(false);

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
    

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            //gameCompleted.gameCompletePanel.SetActive(false);
        }
        else
        {
            Debug.Log("No more levels.");
            // Optionally go to main menu
        }
    }



    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

  /*  public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameCompleted = GameObject.FindObjectOfType<GameCompleted>();
        startTime = Time.time; // Reset timer for new level

        // Reset score and update UI
        ResetScore();
        hasSpawnedBar = false;
    }
  */

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameCompleted = GameObject.FindObjectOfType<GameCompleted>();
        startTime = Time.time;

        if (gameCompleted != null)
        {
            // Reset internal flags manually
            typeof(GameCompleted)
                .GetField("barInitialized", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(gameCompleted, false);

            typeof(GameCompleted)
                .GetField("hasCompleted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(gameCompleted, false);

            if (gameCompleted.gameCompletePanel != null)
                gameCompleted.gameCompletePanel.SetActive(false);

            if (gameCompleted.finishBar != null)
                gameCompleted.finishBar.SetActive(false);
        }

        ResetScore();
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
