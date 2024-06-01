using UnityEngine;
using System.Collections;

public class SmoothRandomRotation : MonoBehaviour
{
    // Duration for the rotation transition
    public float rotationDuration = 2.0f;
    // Time to wait before starting the next rotation
    public float waitTime = 1.0f;

    void Start()
    {
        // Start the coroutine to continuously rotate the GameObject
        StartCoroutine(RotationRoutine());
    }

    IEnumerator RotationRoutine()
    {
        while (true)
        {
            // Generate a random target rotation
            Quaternion targetRotation = Random.rotation;

            // Smoothly interpolate from the current rotation to the target rotation
            Quaternion initialRotation = transform.rotation;
            float elapsedTime = 0f;

            while (elapsedTime < rotationDuration)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final rotation is set
            transform.rotation = targetRotation;

            // Wait for a short period before starting the next rotation transition
            yield return new WaitForSeconds(waitTime);
        }
    }
}
