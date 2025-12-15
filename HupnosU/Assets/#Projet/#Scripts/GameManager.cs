using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string winSceneName = "Win";
    public string gameOverSceneName = "GameOver";
    public FadeManager fadeManager;
    public bool isPaused;
    public bool gameEnded;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!gameEnded && Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 1f;
        fadeManager?.FadeToScene(winSceneName);
    }

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 1f;
        fadeManager?.FadeToScene(gameOverSceneName);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Labyrinth");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
