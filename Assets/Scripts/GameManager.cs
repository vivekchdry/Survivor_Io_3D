using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab; // Player prefab
    [SerializeField] private GameObject enemyPrefab;  // Enemy prefab
    [SerializeField] private List<Transform> enemySpawnPoints; // Spawn points for enemies

    private PlayerManager playerManager;
    private List<EnemyManager> enemyManagers = new List<EnemyManager>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        InstantiatePlayer();
        SpawnEnemies(5); // Example: Spawn 5 enemies initially
    }

    public void InstantiatePlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        playerManager = player.GetComponent<PlayerManager>();

        // Subscribe to the player's death event
        PlayerHealth.OnPlayerDeath += OnPlayerDied;
    }

    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if(i >= count-1)
                break;
            //Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            Transform spawnPoint = enemySpawnPoints[i];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
            RegisterEnemyManager(enemyManager);
        }
    }

    public void RegisterEnemyManager(EnemyManager enemyManager)
    {
        if (!enemyManagers.Contains(enemyManager))
        {
            enemyManagers.Add(enemyManager);
        }
    }

    public void UnregisterEnemyManager(EnemyManager enemyManager)
    {
        if (enemyManagers.Contains(enemyManager))
        {
            enemyManagers.Remove(enemyManager);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid potential memory leaks
        PlayerHealth.OnPlayerDeath -= OnPlayerDied;
    }

    public void OnPlayerDied()
    {
        Debug.Log("Player has died. Restarting game...");

        // Restart logic or game over handling
        // Example: Restart the current scene
        // UnityEngine.SceneManagement.SceneManager.LoadScene("SceneName");

        // Or trigger any other logic you want upon player death
    }
}
