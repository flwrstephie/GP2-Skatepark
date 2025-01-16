using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Vector3 offset;   // Offset position of the camera relative to the player
    public float smoothSpeed = 0.125f; // Smoothing speed for the camera movement

    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Smoothly move the camera to follow the player
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Ensure the camera looks at the player
        transform.LookAt(player);
    }
}