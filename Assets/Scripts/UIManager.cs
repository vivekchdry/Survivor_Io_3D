using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI scoreText;

    private bool isPaused = false;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreDisplay; // Subscribe to score updates
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreDisplay; // Unsubscribe to prevent memory leaks
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    private void UpdateScoreDisplay(int newScore)
    {
        // Update the score text in the UI
        scoreText.text = "Score: " + newScore;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}