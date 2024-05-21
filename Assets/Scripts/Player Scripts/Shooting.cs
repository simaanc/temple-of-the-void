using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign this in the inspector
    public float projectileSpeed = 10f;
    public float shootingCooldown = 0.5f; // Cooldown in seconds between shots
    private Camera mainCamera;
    private Animator animator;
    private bool canShoot = true; // Flag to control shooting based on cooldown
    public Vector2 spawnOffset = new Vector2(1.2f, 0.2f); // Offset for the projectile spawn position
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void OnFire(InputValue value)
    {
        // Check if the fire button was pressed and if shooting is allowed
        if (value.isPressed && canShoot)
        {
            FireProjectile();
            StartCoroutine(ShootingCooldown());
        }
    }

    private void FireProjectile()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z));
        Vector2 adjustedSpawnOffset = spawnOffset;

        // Adjust the spawn offset based on the player's facing direction
        if (spriteRenderer.flipX)
        {
            adjustedSpawnOffset.x = -spawnOffset.x; // Mirror the spawn offset if the player is facing left
        }

        Vector2 spawnPosition = (Vector2)transform.position + adjustedSpawnOffset; // Adjust spawn position with offset
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if (projectileRigidbody != null)
        {
            Vector2 direction = (mouseWorldPosition - spawnPosition).normalized;
            projectileRigidbody.velocity = direction * projectileSpeed;

            // Calculate and adjust the angle for projectile orientation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 45); // Adjusted angle calculation

            // Update the player's facing direction
            UpdatePlayerFacingDirection(direction);
        }

        StartCoroutine(HandleShootingAnimation());
    }

    private void UpdatePlayerFacingDirection(Vector2 direction)
    {
        // Mirror the player character based on the shooting direction
        if (direction.x > 0)
        {
            // Facing right
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            // Facing left
            spriteRenderer.flipX = true;
        }
    }

    IEnumerator HandleShootingAnimation()
    {
        animator.SetBool("IsShooting", true);
        yield return new WaitForSeconds(0.1f); // Wait for one loop of the animation
        animator.SetBool("IsShooting", false);
    }

    IEnumerator ShootingCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingCooldown); // Wait for the cooldown period before allowing another shot
        canShoot = true;
    }
}