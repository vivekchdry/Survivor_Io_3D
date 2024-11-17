using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public Vector3 offset; // Offset from the player

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void OnEnable()
    {
        // Subscribe to player movement updates
        PlayerManager.OnPlayerMoved += UpdateTargetPosition;
    }

    void OnDisable()
    {
        // Unsubscribe from player movement updates
        PlayerManager.OnPlayerMoved -= UpdateTargetPosition;
    }

    void LateUpdate()
    {
        if (targetPosition == null) return;

        // Compute desired position by applying the offset, but keep the camera's height independent
        Vector3 desiredPosition = new Vector3(targetPosition.x + offset.x, offset.y, targetPosition.z + offset.z); // Maintain fixed Y-axis height from offset

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

    private void UpdateTargetPosition(Vector3 playerPosition)
    {
        // Only update X and Z of target position, keep Y fixed by offset
        targetPosition = playerPosition;
        //Debug.Log(targetPosition);
    }
}