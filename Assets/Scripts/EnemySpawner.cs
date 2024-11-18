using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 15f;
    [SerializeField] private float fixedYPosition = 1f;
    [SerializeField] private int maxEnemies = 10;

    private List<EnemyManager> activeEnemies = new List<EnemyManager>();

    public Transform playerTransform;
    
    public void SpawnEnemy()
    {
        if (activeEnemies.Count >= maxEnemies)
            return;

        Vector3 spawnPosition;
        bool positionValid;

        // Generate a random spawn position
        do
        {
            positionValid = true;
            float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 randomDirection = Random.insideUnitSphere.normalized * spawnDistance;
            randomDirection.y = 0; // Ensure enemy spawns on a flat plane
            spawnPosition = playerTransform.position + randomDirection;
            spawnPosition.y = fixedYPosition;

            // Ensure there's no overlapping with other enemies
            foreach (var enemy in activeEnemies)
            {
                if (Vector3.Distance(spawnPosition, enemy.transform.position) < 2f)
                {
                    positionValid = false;
                    break;
                }
            }
        } while (!positionValid);

        // Instantiate enemy
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyManager enemyManager = newEnemy.GetComponent<EnemyManager>();
        enemyManager.playerTransform = playerTransform;
        if (enemyManager != null)
        {
            activeEnemies.Add(enemyManager);
            enemyManager.OnEnemyDeath += HandleEnemyDeath; // Subscribe to death event
        }
    }

    private void HandleEnemyDeath(EnemyManager enemyManager)
    {
        // Unregister the enemy
        if (activeEnemies.Contains(enemyManager))
        {
            activeEnemies.Remove(enemyManager);
            enemyManager.OnEnemyDeath -= HandleEnemyDeath; // Unsubscribe from event
        }
    }

    private void OnDisable()
    {
        // Clean up events to avoid memory leaks
        foreach (var enemyManager in activeEnemies)
        {
            enemyManager.OnEnemyDeath -= HandleEnemyDeath;
        }
    }
}
