using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportOnRightClick : MonoBehaviour
{
    public float maxTeleportDistance = 5f;
    public float cooldown = 2f; 
    private float lastTeleportTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Initialize lastTeleportTime to allow immediate teleportation at the start
        lastTeleportTime = Time.time - cooldown;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        if (Time.time - lastTeleportTime >= cooldown && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Camera.main.transform.position.z));
            mousePosition.z = transform.position.z; 

            Vector3 direction = (mousePosition - transform.position).normalized;
            float distanceToMouse = Vector3.Distance(transform.position, mousePosition);
            float teleportDistance = Mathf.Min(maxTeleportDistance, distanceToMouse);

            transform.position += direction * teleportDistance;
            lastTeleportTime = Time.time;

            // Update the player's facing direction based on the teleportation direction
            UpdatePlayerFacingDirection(direction);
        }
    }

    private void UpdatePlayerFacingDirection(Vector3 direction)
    {
        // Mirror the player character based on the teleportation direction
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
}