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

        // Set random attack and retreat directions
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

        if (distance > attackDistance)
        {
            transform.position += attackDirection * swoopSpeed * Time.deltaTime;
        }
    }

    void Retreat()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < retreatDistance)
        {
            transform.position += retreatDirection * retreatSpeed * Time.deltaTime;
        }
    }
}
