using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action<Vector2> OnMovementInput; // Notifies about movement input (x, y).
    public static Action OnJumpInput;             // Notifies when the jump button is pressed.

    public FloatingJoystick joystick; // Reference to the floating joystick.

    void Update()
    {
        // Notify movement input
        Vector2 movementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (movementInput.magnitude > 0.1f)
        {
            OnMovementInput?.Invoke(movementInput);
        }
        else
        {
            OnMovementInput?.Invoke(Vector2.zero);
        }

        // Notify jump input
        if (Input.GetButtonDown("Jump")) // Jump button mapped in Input Manager
        {
            OnJumpInput?.Invoke();
        }
    }
    private void HandleMovementInput(Vector2 movementInput)
    {
        // Clamp input values to avoid small drifting
        if (movementInput.magnitude < 0.1f)
        {
            movementInput = Vector2.zero; // Neutral input
        }

        OnMovementInput?.Invoke(movementInput);
    }

}