using UnityEngine;

public class HealthObserver : MonoBehaviour
{
    // [SerializeField] private GameObject coinPrefab; // Coin prefab for spawning
    //
    // private void OnEnable()
    // {
    //     PlayerHealth.OnPlayerDied += HandlePlayerDeath;
    //     EnemyHealth.OnEnemyDied += SpawnCoin;
    // }
    //
    // private void OnDisable()
    // {
    //     PlayerHealth.OnPlayerDied -= HandlePlayerDeath;
    //     EnemyHealth.OnEnemyDied -= SpawnCoin;
    // }
    //
    // private void HandlePlayerDeath()
    // {
    //     Debug.Log("Player died. Restarting the game.");
    //     // Add logic to restart the game, e.g., reload the scene:
    //     // UnityEngine.SceneManagement.SceneManager.LoadScene("YourSceneName");
    // }
    //
    // private void SpawnCoin(Vector3 enemyPosition)
    // {
    //     if (coinPrefab != null)
    //     {
    //         Instantiate(coinPrefab, enemyPosition, Quaternion.identity);
    //     }
    // }
}