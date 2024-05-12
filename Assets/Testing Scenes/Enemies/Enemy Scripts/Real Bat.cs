using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float avoidDistance = 2f;
    public LayerMask obstacleLayer;
    public float stuckThreshold = 0.1f;
    public float unstuckDuration = 1f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private bool avoidingObstacle = false;
    private bool isStuck = false;
    private Vector3 lastPosition;
    private float unstuckTimer = 0f;

    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Disable gravity for the bat
        rb.gravityScale = 0f;
        // Start the bat moving initially
        GenerateRandomDirection();

        // Store the original scale of the bat
        originalScale = transform.localScale;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!avoidingObstacle)
        {
            // Check for obstacles (tilemap colliders) and avoid them
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, avoidDistance, obstacleLayer);
            if (hit.collider != null)
            {
                // Change direction away from the obstacle
                Vector2 avoidDirection = (hit.point - (Vector2)transform.position).normalized;
                movementDirection = Vector2.Reflect(movementDirection, avoidDirection);
                avoidingObstacle = true;
                Invoke("ResetAvoidance", 0.5f); // Delay reset to prevent immediate re-avoidance
            }
        }

        // Flip the bat's model based on movement direction
        if (movementDirection.x < 0)
        {
            // Flip the bat's scale horizontally
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            // Reset the bat's scale to its original direction
            transform.localScale = originalScale;
        }

        // Check if the bat is stuck
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, lastPosition);
        if (distance < stuckThreshold)
        {
            isStuck = true;
            unstuckTimer += Time.deltaTime;
            if (unstuckTimer >= unstuckDuration)
            {
                GenerateRandomDirection();
                isStuck = false;
                unstuckTimer = 0f;
            }
        }
        else
        {
            isStuck = false;
            unstuckTimer = 0f;
        }
        lastPosition = currentPosition;
    }

    void FixedUpdate()
    {
        // Move the bat
        rb.velocity = movementDirection * moveSpeed;
    }

    void GenerateRandomDirection()
    {
        // Generate a random direction for the bat to move in
        movementDirection = Random.insideUnitCircle.normalized;
    }

    void ResetAvoidance()
    {
        avoidingObstacle = false;
        GenerateRandomDirection(); // Generate new random direction after avoiding obstacle
    }
}
