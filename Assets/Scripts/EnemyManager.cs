using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Prefab to spawn
    [SerializeField] private GameObject healthPickup; // Example additional prefab

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        enemyHealth.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        enemyHealth.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(Vector3 position)
    {
        Debug.Log("Enemy died. Spawning item.");

        // Example logic: spawn a coin or power-up based on some condition
        if (Random.value > 0.5f && coinPrefab != null)
        {
            Instantiate(coinPrefab, position, Quaternion.identity);
        }
        else if (healthPickup != null)
        {
            Instantiate(healthPickup, position, Quaternion.identity);
        }
    }
}