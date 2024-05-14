using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportOnRightClick : MonoBehaviour
{
    public float maxTeleportDistance = 5f;
    public float cooldown = 2f; 
    public float lastTeleportTime = -2f; 

    void Update()
    {
        if (Time.time - lastTeleportTime >= cooldown && Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z - transform.position.z));
            mousePosition.z = transform.position.z; 

            Vector3 direction = (mousePosition - transform.position).normalized;
            float distanceToMouse = Vector3.Distance(transform.position, mousePosition);
            float teleportDistance = Mathf.Min(maxTeleportDistance, distanceToMouse);

            transform.position += direction * teleportDistance;
            lastTeleportTime = Time.time; 
        }
    }
}
