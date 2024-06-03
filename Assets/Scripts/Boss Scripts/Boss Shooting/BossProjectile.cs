using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    public float lifetime = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // Destroy the projectile after a set lifetime
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile hit: " + other.name + " at: " + Time.time);
        if (other.CompareTag("Player") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
