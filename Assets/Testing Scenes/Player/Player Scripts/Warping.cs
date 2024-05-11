using UnityEngine;

public class TeleportOnRightClick : MonoBehaviour
{
    public Transform teleportTarget; // The target position to teleport to
    public float teleportDistanceThreshold = 1f; // The maximum distance the player can teleport

    private bool isRightClicking = false;
    private Vector2 originalPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Check if right mouse button is clicked
        {
            isRightClicking = true;
            originalPosition = transform.position;
        }

        if (Input.GetMouseButtonUp(1)) // Check if right mouse button is released
        {
            isRightClicking = false;

            // Calculate the distance between the original position and current mouse position
            float distance = Vector2.Distance(originalPosition, transform.position);

            // Check if the distance is less than the threshold
            if (distance <= teleportDistanceThreshold)
            {
                // Teleport the player to the target position
                transform.position = teleportTarget.position;
            }
        }

        if (isRightClicking)
        {
            // Continuously update the position of the player while right click is held
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            Debug.DrawRay(transform.position, direction * teleportDistanceThreshold, Color.red); // Draw debug line

            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }
}
