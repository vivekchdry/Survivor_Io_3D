using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static Action<Vector3> OnPlayerMoved; // Event for player position updates
    [SerializeField] private Image healthBarFill; // Health bar UI reference

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private Vector2 currentMovementInput;
    private bool jumpRequested;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        // Subscribe to InputManager events
        InputManager.OnMovementInput += HandleMovementInput;
        InputManager.OnJumpInput += HandleJumpInput;

        // Subscribe to PlayerHealth events
        playerHealth.OnHealthChanged += UpdateHealthBar;
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        // Unsubscribe from InputManager events
        InputManager.OnMovementInput -= HandleMovementInput;
        InputManager.OnJumpInput -= HandleJumpInput;

        // Unsubscribe from PlayerHealth events
        playerHealth.OnHealthChanged -= UpdateHealthBar;
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
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

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        // Update the health bar fill amount
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
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
            Coin coin = hit.collider.GetComponent<Coin>();
            if (coin != null)
            {
                coin.Collect();
            }
        }
        else if (hit.collider.CompareTag("HealthPickup"))
        {
            HealthPickup healthPickup = hit.collider.GetComponent<HealthPickup>();
            if (healthPickup != null)
            {
                healthPickup.Collect();
            }
        }
    }
}
