using UnityEngine;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;          // In-Game (oben rechts)
    public TextMeshProUGUI gameOverScoreText;  // Game Over Panel
    public TextMeshProUGUI mainMenuScoreText;  // Main Menu Panel

    string filePath;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        filePath = Path.Combine(Application.dataPath, "highscore.txt");
    }

    void Start()
    {
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

    public void SaveScoreToFile()
    {
        int oldHighscore = 0;

        // wenn Datei existiert -> alten Wert lesen
        if (File.Exists(filePath))
            int.TryParse(File.ReadAllText(filePath), out oldHighscore);

        // nur speichern wenn neuer Score höher
        if (score > oldHighscore)
            File.WriteAllText(filePath, score.ToString());

        // GameOver Panel Text setzen
        if (gameOverScoreText != null)
            gameOverScoreText.text = "Your Score: " + score;
    }

    void LoadHighscore()
    {
        if (mainMenuScoreText == null) return;

        if (File.Exists(filePath))
            mainMenuScoreText.text = "Highscore: " + File.ReadAllText(filePath);
        else
            mainMenuScoreText.text = "Highscore: 0";
    }

    public void ResetHighscore()
    {
        if (File.Exists(filePath))
            File.WriteAllText(filePath, "0"); // Datei leeren
        LoadHighscore();
    }
}
