using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab; // Player prefab
    [SerializeField] private EnemySpawner enemySpawner; // Reference to the EnemySpawner

    public PlayerManager playerManager;

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
        InvokeRepeating(nameof(SpawnEnemies), 3f, 3f); // Spawn enemies every 3 seconds
    }

    public void InstantiatePlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        playerManager = player.GetComponent<PlayerManager>();
        enemySpawner.playerTransform = player.transform;

        PlayerHealth.OnPlayerDeath += OnPlayerDied; // Subscribe to player death event
    }

    private void SpawnEnemies()
    {
        enemySpawner.SpawnEnemy();
    }

    public void OnPlayerDied()
    {
        Debug.Log("Player has died. Restarting game...");
    }
}