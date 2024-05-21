using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab; // The smaller projectile prefab to shoot
    public float shootForce = 5f; // The force with which the projectile is shot
    public float maxLifetime = 2f; // Maximum lifetime of the projectiles
    public float minScale = 0.5f; // Minimum scale of the projectiles
    public float maxScale = 1f; // Maximum scale of the projectiles

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change to your desired input key
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Random scale for the projectile
        float scale = Random.Range(minScale, maxScale);
        projectile.transform.localScale = new Vector3(scale, scale, 1f);

        // Random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        
        // Apply force to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = randomDirection * shootForce;

        // Destroy after maxLifetime
        Destroy(projectile, maxLifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the projectile on collision with anything
        Destroy(collision.gameObject);
    }
}
