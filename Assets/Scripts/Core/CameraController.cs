using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 aheadDistance;
    [SerializeField] private float cameraSpeed;

    private Transform thisTransform;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {
        if (player == null)
        {
            // If the player is not assigned, you can find the player in the scene using a "Player" tag, for example.
            player = GameObject.FindGameObjectWithTag("Player").transform;

            if (player == null)
            {
                Debug.LogWarning("Player not found. Assign the player transform in the inspector or tag your player object.");
                return;
            }
        }

        // Calculate the target position for the camera
        float targetX = player.position.x + aheadDistance.x;
        float targetY = player.position.y + aheadDistance.y;

        // Smoothly move the camera towards the target position
        thisTransform.position = Vector3.SmoothDamp(thisTransform.position, new Vector3(targetX, targetY, thisTransform.position.z), ref currentVelocity, cameraSpeed * Time.deltaTime);
    }
}
