using UnityEngine;

public class TeleportOnRightClick : MonoBehaviour
{
    public float maxTeleportDistance = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z - transform.position.z));
            mousePosition.z = transform.position.z;

            Vector3 direction = (mousePosition - transform.position).normalized;

            float distanceToMouse = Vector3.Distance(transform.position, mousePosition);
            float teleportDistance = Mathf.Min(maxTeleportDistance, distanceToMouse);

            transform.position = transform.position + direction * teleportDistance;
        }
    }
}