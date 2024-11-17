using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public static event Action<int> OnScoreChanged; // Notify UIManager of score changes

    private int currentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += AddScore; // Subscribe to coin collection events
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= AddScore; // Unsubscribe to prevent memory leaks
    }

    private void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        // Notify UIManager about the score change
        OnScoreChanged?.Invoke(currentScore);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}