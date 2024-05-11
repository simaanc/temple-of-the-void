using UnityEngine;

public class BatController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the bat moves
    public float changeDirectionInterval = 2f; // Time interval to change direction
    public float maxFlightDistance = 5f; // Maximum distance the bat can fly from its starting position
    public float playerDetectionRadius = 5f; // Radius for detecting the player
    public LayerMask playerLayer; // Layer mask for the player
    public Transform player; // Reference to the player's transform

    private Vector3 startingPosition; // Starting position of the bat
    private Vector3 targetPosition; // Current target position for the bat to fly towards
    private float changeDirectionTimer; // Timer to keep track of changing direction
    private Rigidbody2D rb; // Rigidbody component for collision detection

    void Start()
    {
        // Store the starting position of the bat
        startingPosition = transform.position;
        // Initialize the timer
        changeDirectionTimer = changeDirectionInterval;
        // Set the initial target position
        SetNewTargetPosition();
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is within the detection radius
        if (player != null && Vector3.Distance(transform.position, player.position) <= playerDetectionRadius)
        {
            // Set the player position as the target position
            targetPosition = player.position;
        }

        // Move the bat towards the target position
        Vector2 moveDirection = (targetPosition - transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;

        // Check for obstacles in the path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, maxFlightDistance, playerLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // If the player is detected, set the player position as the target position
            targetPosition = hit.collider.transform.position;
        }

        // Check if the bat has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Set a new target position
            SetNewTargetPosition();
        }

        // Update the change direction timer
        changeDirectionTimer -= Time.deltaTime;

        // Check if it's time to change direction
        if (changeDirectionTimer <= 0f)
        {
            // Set a new target position
            SetNewTargetPosition();
            // Reset the timer
            changeDirectionTimer = changeDirectionInterval;
        }
    }

    // Function to set a new random target position for the bat
    void SetNewTargetPosition()
    {
        // Calculate a random direction for the bat to fly towards
        Vector3 randomDirection = Random.insideUnitCircle.normalized * maxFlightDistance;
        // Set the target position as a point relative to the starting position
        targetPosition = startingPosition + randomDirection;
    }
}
