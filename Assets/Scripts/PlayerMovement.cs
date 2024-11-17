using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float downForce = 2f;
    public float gravity = 9.8f;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 movementInput;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetMovementInput(Vector2 input)
    {
        // Stop movement if input is negligible
        movementInput = input.magnitude < 0.1f ? Vector3.zero : new Vector3(input.x, 0f, input.y);
    }

    public void TriggerJump()
    {
        if (isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    private void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Process movement
        if (movementInput.magnitude > 0.1f)
        {
            Vector3 move = movementInput.normalized * moveSpeed * Time.deltaTime;
            controller.Move(move);

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime * downForce;

        // Move with gravity
        controller.Move(velocity * Time.deltaTime);
    }
}