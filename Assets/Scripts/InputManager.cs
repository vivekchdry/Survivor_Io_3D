using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> OnMovementInput; // Notifies movement input
    public static event Action OnJumpInput;             // Notifies jump input

    [SerializeField] private FloatingJoystick joystick; // Floating joystick reference

    private void Update()
    {
        // Notify movement input
        Vector2 movementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        OnMovementInput?.Invoke(movementInput.magnitude > 0.1f ? movementInput : Vector2.zero);

        // Notify jump input
        if (Input.GetButtonDown("Jump")) // Ensure "Jump" is mapped in Unity's Input Manager
        {
            OnJumpInput?.Invoke();
        }
    }
}