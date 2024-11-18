using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Prefab for coins
    [SerializeField] private GameObject healthPickup; // Prefab for health pickups
    [SerializeField] private float rewardDropChance = 0.5f; // 50% chance to drop a reward
    [SerializeField] private float speed = 2f; // Movement speed
    [SerializeField] private float attackCooldown = 2f; // Cooldown between attacks
    [SerializeField] private int damageAmount = 10; // Damage dealt to the player
    [SerializeField] private bool canMove = true;
    
    private EnemyHealth enemyHealth;
    private float lastAttackTime;
    public event Action<EnemyManager> OnEnemyDeath; // Notify spawner of death
    public Transform playerTransform { get; set; }
    

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

    private void Update()
    {
        // Check if the enemy is dead
        if (enemyHealth.IsDead)
        {
            return;
        }
        // Calculate direction
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Ignore vertical movement
        
        if(Vector3.Distance(transform.position, playerTransform.position) > 1f)
            canMove = true;
        if (!canMove)
            return;
        // Move towards the player
        transform.position += direction * speed * Time.deltaTime;

        // Face the player
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }


    private void HandleEnemyDeath()
    {
        gameObject.SetActive(false);
        
        // Notify the spawner
        OnEnemyDeath?.Invoke(this);

        // Spawn a reward
        SpawnReward();

        // Destroy the enemy
        Destroy(gameObject);
    }

    private void SpawnReward()
    {
        // // Randomly decide whether to drop a coin or a health pickup
        // if (Random.value < rewardDropChance)
        // {
        //     GameObject rewardPrefab = (Random.value > 0.5f) ? coinPrefab : healthPickup;
        //     if (rewardPrefab != null)
        //     {
        //         Instantiate(rewardPrefab, transform.position, Quaternion.identity);
        //         
        //     }
        // }
        
        if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
    
}