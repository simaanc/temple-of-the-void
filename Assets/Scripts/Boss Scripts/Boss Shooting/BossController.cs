using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float fireRate = 2f; // Time between shots in seconds
    private float nextFireTime = 0f;

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ShootProjectile()
    {
        Debug.Log("Shooting Projectile at: " + Time.time);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();
        projScript.SetTarget(player);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
