using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign this in the inspector
    public float projectileSpeed = 10f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void OnFire(InputValue value)
    {
        // Check if the right mouse button was pressed
        if (value.isPressed)
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null) return;

        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z));

        Vector2 spawnPosition = transform.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if (projectileRigidbody != null)
        {
            Vector2 direction = (mouseWorldPosition - spawnPosition);
            projectileRigidbody.velocity = direction.normalized * projectileSpeed;

            // Calculate the angle from the direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float offsetAngle = 45; // Adjust as necessary
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle + offsetAngle - 90);
        }
    }
}