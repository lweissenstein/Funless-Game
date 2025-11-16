using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject MainMenuPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    private bool isPaused = false;
    private static bool autoStartAfterLoad = false; // Flag für Retry

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (autoStartAfterLoad)
        {
            autoStartAfterLoad = false;
            StartGame();
        }
        else
        {
            ShowMainMenu();
        }
    }

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
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RetryGame()
    {
        autoStartAfterLoad = true; // Flag setzen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        autoStartAfterLoad = false; // Flag löschen
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

