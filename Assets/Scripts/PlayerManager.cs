using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static Action<Vector3> OnPlayerMoved; // Event for player position updates
    [SerializeField] private HealthBar healthBar; // Health bar UI reference

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private Vector2 currentMovementInput;
    private bool jumpRequested;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    
    private void Start()
    {
        // Initialize the health bar with full health
        if (playerHealth != null)
        {
            HandleHealthChange(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }
    }

    private void OnEnable()
    {
        // Subscribe to InputManager events
        InputManager.OnMovementInput += HandleMovementInput;
        InputManager.OnJumpInput += HandleJumpInput;

        // Subscribe to PlayerHealth events
        PlayerHealth.OnHealthChanged += HandleHealthChange;
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
        HealthPickup.OnHealthCollected += HandleHealthPickup;
    }

    private void OnDisable()
    {
        // Unsubscribe from InputManager events
        InputManager.OnMovementInput -= HandleMovementInput;
        InputManager.OnJumpInput -= HandleJumpInput;

        // Unsubscribe from PlayerHealth events
        PlayerHealth.OnHealthChanged -= HandleHealthChange;
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
        HealthPickup.OnHealthCollected -= HandleHealthPickup;
    }

    private void Update()
    {
        // Pass movement input to PlayerMovement
        if (currentMovementInput.magnitude > 0.1f)
        {
            playerMovement.SetMovementInput(currentMovementInput);
            OnPlayerMoved?.Invoke(transform.position); // Notify position changes
        }
        else
        {
            playerMovement.SetMovementInput(Vector2.zero);
        }

        // Handle jump input
        if (jumpRequested)
        {
            playerMovement.TriggerJump();
            jumpRequested = false; // Reset jump request
        }
    }

    private void HandleMovementInput(Vector2 movementInput)
    {
        currentMovementInput = movementInput;
    }

    private void HandleJumpInput()
    {
        jumpRequested = true;
    }

    private void HandleHealthChange(int currentHealth, int maxHealth)
    {
        if (healthBar != null)
        {
            // Calculate the health percentage and pass it to the HealthBar
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBar.UpdateFill(healthPercentage);
        }
    }

    private void HandleHealthPickup(int healAmount)
    {
        Debug.Log($"Player healed by {healAmount}.");
        playerHealth.Heal(healAmount);
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player has died. Notifying GameManager.");
        GameManager.Instance.OnPlayerDied();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Coin"))
        {
            Debug.Log($"Player hit {hit.collider.gameObject.name}");
            Coin coin = hit.collider.GetComponent<Coin>();
            if (coin != null)
            {
                coin.Collect();
            }
        }
        else if (hit.collider.CompareTag("HealthPickup"))
        {
            Debug.Log($"Player hit {hit.collider.gameObject.name}");
            HealthPickup healthPickup = hit.collider.GetComponent<HealthPickup>();
            if (healthPickup != null)
            {
                healthPickup.Collect();
            }
        }
    }
}
