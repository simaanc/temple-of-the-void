using UnityEngine;

public class BatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = GetRandomDirection();
    }

    void FixedUpdate()
    {
        Vector2 avoidanceDirection = AvoidCollisions();
        if (avoidanceDirection != Vector2.zero)
        {
            moveDirection = avoidanceDirection;
        }
        else if (Random.value < 0.02f) // Change direction randomly
        {
            moveDirection = GetRandomDirection();
        }

        rb.velocity = moveDirection * moveSpeed;
        RotateTowards(moveDirection);
    }

    Vector2 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }

    void RotateTowards(Vector2 targetDirection)
    {
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    Vector2 AvoidCollisions()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 2f); // Adjust the distance as needed
        if (hit.collider != null && hit.collider.tag == "TilemapCollider")
        {
            Vector2 avoidanceDirection = Quaternion.AngleAxis(90, Vector3.forward) * hit.normal;
            return avoidanceDirection.normalized;
        }
        return Vector2.zero;
    }
}
