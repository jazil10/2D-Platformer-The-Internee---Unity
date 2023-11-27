using UnityEngine;

public class CatFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 5.0f;
    [SerializeField] private float followDistance = 2.0f;
    [SerializeField] private Animator catAnimator;
    [SerializeField] private LayerMask groundLayer; // Specify the ground layer.

    private Vector3 initialPosition;
    private Vector3 initialScale;

    private void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Calculate the target position based on the player's position
        Vector3 targetPosition = player.position;

        // Calculate the distance to the player
        float distanceToPlayer = Mathf.Abs(targetPosition.x - transform.position.x);

        if (distanceToPlayer < followDistance)
        {
            // Determine if the player is moving
            float playerVelocity = player.GetComponent<Rigidbody2D>().velocity.x;

            if (Mathf.Abs(playerVelocity) > 0.1f)
            {
                catAnimator.SetBool("run", true);

                // Flip the cat's orientation based on the player's movement direction
                if (playerVelocity > 0)
                {
                    transform.localScale = initialScale;
                }
                else if (playerVelocity < 0)
                {
                    transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
                }
            }
            else
            {
                catAnimator.SetBool("run", false);
            }
        }

        // Cast a ray down to check for ground and adjust the cat's Y position.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit.collider != null)
        {
            // Ensure that the cat is on the ground.
            targetPosition.y = hit.point.y;
        }

        // Check if the player is above the cat and adjust the cat's Y position.
        if (player.position.y > transform.position.y)
        {
            targetPosition.y = player.position.y;
        }

        // Smoothly move the cat toward the player.
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
