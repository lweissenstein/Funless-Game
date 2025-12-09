using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Don't forget this!

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("Text References")]
    public TextMeshProUGUI inGameScoreText;     // Drag 'inGameScore' here in Inspector
    public TextMeshProUGUI gameOverScoreText;   // Drag 'GameOverScore' here in Inspector
    public TextMeshProUGUI mainMenuScoreText;   // Drag 'MainMenuHighScore' here in Inspector

    private bool isPaused = false;
    public static bool isRetry = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // FIX: Push references to ScoreManager immediately when scene starts
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.RefreshReferences(inGameScoreText, gameOverScoreText, mainMenuScoreText);
        }

        if (isRetry)
        {
            MainMenuPanel.SetActive(false);
            PausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
            Time.timeScale = 1f;
            isRetry = false;
        }
        else
        {
            MainMenuPanel.SetActive(true);
            PausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    // ... Rest of your existing methods (ShowMainMenu, StartGame, etc.) stay the same ...

    public void ShowMainMenu()
    {
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;

        ScoreManager.Instance.ResetScore();
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RetryGame()
    {
        isRetry = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ScoreManager.Instance.ResetScore();
    }

    public void ReturnToMainMenu()
    {
        // This reloads the scene, which triggers Start() again, 
        // causing references to be refreshed correctly.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }
    }
}