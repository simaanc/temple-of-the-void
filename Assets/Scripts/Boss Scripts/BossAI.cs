using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float minSwoopSpeed = 3f;
    public float maxSwoopSpeed = 7f;
    public float minRetreatSpeed = 2f;
    public float maxRetreatSpeed = 5f;
    public float minAttackDistance = 1f;
    public float maxAttackDistance = 3f;
    public float minRetreatDistance = 4f;
    public float maxRetreatDistance = 6f;
    public float minAttackDuration = 1f;
    public float maxAttackDuration = 3f;
    public float minRetreatDuration = 2f;
    public float maxRetreatDuration = 4f;
    public float minAngleOffset = -30f;
    public float maxAngleOffset = 30f;
    public LayerMask obstacleLayer;  // Layer mask for tagged tiles
    public float obstacleAvoidanceDistance = 1f; // Minimum distance to keep from obstacles
    public int raysCount = 8; // Number of rays to cast for obstacle detection

    private bool isAttacking = true;
    private float timer;
    private float swoopSpeed;
    private float retreatSpeed;
    private float attackDistance;
    private float retreatDistance;
    private Vector3 attackDirection;
    private Vector3 retreatDirection;

    void Start()
    {
        SetRandomParameters();
        timer = Random.Range(minAttackDuration, maxAttackDuration);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (isAttacking)
        {
            SwoopIn();
            if (timer <= 0)
            {
                isAttacking = false;
                SetRandomParameters();
                timer = Random.Range(minRetreatDuration, maxRetreatDuration);
            }
        }
        else
        {
            Retreat();
            if (timer <= 0)
            {
                isAttacking = true;
                SetRandomParameters();
                timer = Random.Range(minAttackDuration, maxAttackDuration);
            }
        }
    }

    void SetRandomParameters()
    {
        swoopSpeed = Random.Range(minSwoopSpeed, maxSwoopSpeed);
        retreatSpeed = Random.Range(minRetreatSpeed, maxRetreatSpeed);
        attackDistance = Random.Range(minAttackDistance, maxAttackDistance);
        retreatDistance = Random.Range(minRetreatDistance, maxRetreatDistance);

        attackDirection = GetRandomDirection(player.position - transform.position);
        retreatDirection = GetRandomDirection(transform.position - player.position);
    }

    Vector3 GetRandomDirection(Vector3 baseDirection)
    {
        float angleOffset = Random.Range(minAngleOffset, maxAngleOffset);
        return Quaternion.Euler(0, 0, angleOffset) * baseDirection.normalized;
    }

    void SwoopIn()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackDistance && !IsObstacleInDirection(attackDirection))
        {
            MoveInDirection(attackDirection, swoopSpeed);
        }
        else
        {
            AdjustDirection(ref attackDirection);
            MoveInDirection(attackDirection, swoopSpeed);
        }
    }

    void Retreat()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < retreatDistance && !IsObstacleInDirection(retreatDirection))
        {
            MoveInDirection(retreatDirection, retreatSpeed);
        }
        else
        {
            AdjustDirection(ref retreatDirection);
            MoveInDirection(retreatDirection, retreatSpeed);
        }
    }

    bool IsObstacleInDirection(Vector3 direction)
    {
        for (int i = 0; i < raysCount; i++)
        {
            float angle = i * 360f / raysCount;
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * direction;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, obstacleAvoidanceDistance, obstacleLayer);
            if (hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    void MoveInDirection(Vector3 direction, float speed)
    {
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    void AdjustDirection(ref Vector3 direction)
    {
        for (int i = 1; i <= raysCount / 2; i++)
        {
            Vector3 leftDirection = Quaternion.Euler(0, 0, i * (360f / raysCount)) * direction;
            Vector3 rightDirection = Quaternion.Euler(0, 0, -i * (360f / raysCount)) * direction;

            if (!IsObstacleInDirection(leftDirection))
            {
                direction = leftDirection;
                return;
            }
            if (!IsObstacleInDirection(rightDirection))
            {
                direction = rightDirection;
                return;
            }
        }
        direction = -direction; // Fallback if all directions are blocked
    }
}
