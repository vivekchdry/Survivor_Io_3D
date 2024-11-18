using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab
    [SerializeField] private Transform playerBody;    // Reference to player's body
    [SerializeField] private float defensiveRadius = 10f; // Player's defensive zone radius
    [SerializeField] private float fireCooldown = 1f;     // Cooldown between shots
    [SerializeField] private float spawnOffset = 0.5f;    // Offset from player center for spawning bullets
    [SerializeField] private bool shootingEnabled = true;
    
    private bool canShoot = true;

    private void Update()
    {
        if(!shootingEnabled)
            return;
        // Get all enemies within the defensive radius
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, defensiveRadius, LayerMask.GetMask("Enemy"));

        // Find the closest enemy
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        // Shoot at the closest enemy if one is found
        if (closestEnemy != null && canShoot)
        {
            StartCoroutine(ShootAtEnemy(closestEnemy));
        }
    }

    private IEnumerator ShootAtEnemy(Transform enemy)
    {
        canShoot = false;

        // Calculate direction to the enemy
        Vector3 enemyDirection = (enemy.position - playerBody.position).normalized;

        // Calculate spawn position with offset
        Vector3 spawnPosition = playerBody.position + 
                                new Vector3(enemyDirection.x * spawnOffset, 0, enemyDirection.z * spawnOffset);

        // Keep Y position constant to avoid bullets spawning in the ground or sky
        spawnPosition.y = playerBody.position.y;

        // Instantiate bullet and initialize it with the enemy's position
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(enemy.position); // Pass enemy position as the target
            }
        }

        yield return new WaitForSeconds(fireCooldown); // Wait for cooldown before shooting again
        canShoot = true;
    }
}
