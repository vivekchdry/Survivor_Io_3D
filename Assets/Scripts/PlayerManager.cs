using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to PlayerMovement script

    public static Action<Vector3> OnPlayerMoved; // Event for player position updates

    private Vector2 currentMovementInput;
    private bool jumpRequested;

    void OnEnable()
    {
        InputManager.OnMovementInput += HandleMovementInput;
        InputManager.OnJumpInput += HandleJumpInput;
    }

    void OnDisable()
    {
        InputManager.OnMovementInput -= HandleMovementInput;
        InputManager.OnJumpInput -= HandleJumpInput;
    }

    void Update()
    {
        if (currentMovementInput.magnitude > 0.1f)
        {
            playerMovement.SetMovementInput(currentMovementInput);
        }
        else
        {
            playerMovement.SetMovementInput(Vector2.zero);
        }

        if (jumpRequested)
        {
            playerMovement.TriggerJump();
            jumpRequested = false;
        }

        // Broadcast the player's position
        OnPlayerMoved?.Invoke(transform.position);
    }

    private void HandleMovementInput(Vector2 movementInput)
    {
        currentMovementInput = movementInput;
    }

    private void HandleJumpInput()
    {
        jumpRequested = true;
    }
}