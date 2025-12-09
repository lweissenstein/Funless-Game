using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;

    // These references will now be assigned by the UIManager
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI mainMenuScoreText;

    private const string HighscoreKey = "Highscore";

    void Awake()
    {
        // Remove OnSceneLoaded subscription, we will handle this via UIManager now
        // SceneManager.sceneLoaded += OnSceneLoaded; 

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this from UIManager.Start()
    public void RefreshReferences(TextMeshProUGUI gameScore, TextMeshProUGUI overScore, TextMeshProUGUI menuScore)
    {
        scoreText = gameScore;
        gameOverScoreText = overScore;
        mainMenuScoreText = menuScore;

        LoadHighscore();
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void SaveScore()
    {
        int oldHighscore = PlayerPrefs.GetInt(HighscoreKey, 0);

        if (score > oldHighscore)
            PlayerPrefs.SetInt(HighscoreKey, score);

        PlayerPrefs.Save();

        if (gameOverScoreText != null)
            gameOverScoreText.text = "Your Score: " + score;
    }

    // Made public so we can call it after assigning references
    public void LoadHighscore()
    {
        if (mainMenuScoreText == null) return;

        int highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
        mainMenuScoreText.text = "Highscore: " + highscore;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt(HighscoreKey, 0);
        PlayerPrefs.Save(); // FIX: Force save immediately
        LoadHighscore(); // Update the UI immediately
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}