using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Movement speed
    public float jumpForce = 7f;       // Jump force
    public float downForce = 2f;
    public float gravity = 9.8f;       // Gravity applied to the player
    public LayerMask groundLayer;      // Ground layer for grounded check

    private CharacterController controller;
    private Vector3 velocity;          // Tracks player's velocity
    private Vector3 movementInput;     // Stores current movement input
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    public void TriggerJump()
    {
        if (isGrounded)
        {
            velocity.y = jumpForce;
        }
    }
    public void SetMovementInput(Vector2 input)
    {
        // Stop movement if input is negligible
        if (input.magnitude < 0.1f)
        {
            movementInput = Vector3.zero;
        }
        else
        {
            movementInput = new Vector3(input.x, 0f, input.y);
        }
    }

    public void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure the player stays grounded
        }

        // Process movement
        if (movementInput.magnitude > 0.1f)
        {
            Vector3 move = movementInput.normalized * moveSpeed * Time.deltaTime;
            controller.Move(move);

            // Rotate the player to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime * downForce;

        // Apply gravity to movement
        controller.Move(velocity * Time.deltaTime);
    }

}