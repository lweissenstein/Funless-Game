using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject MainMenuPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    private bool isPaused = false;
    public static bool isRetry = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (isRetry)
        {
            // Directly start game after retry
            MainMenuPanel.SetActive(false);
            PausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
            Time.timeScale = 1f;
            isRetry = false; // reset for next time
        }
        else
        {
            // Normal first-time start: show main menu
            MainMenuPanel.SetActive(true);
            PausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void ShowMainMenu()
    {
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 0f; // pause the game
    }

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f; // start/resume game
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RetryGame()
    {
        isRetry = true; // tell UIManager that we want to skip main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = false;
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
