using UnityEngine;
using System.Collections;

public class SmoothRandomScale : MonoBehaviour
{
    // Define the minimum and maximum values for the scale
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 maxScale = new Vector3(2.0f, 2.0f, 2.0f);

    // Duration for the scaling transition
    public float scaleDuration = 2.0f;

    void Start()
    {
        // Start the coroutine to continuously scale the GameObject
        StartCoroutine(ScaleRoutine());
    }

    IEnumerator ScaleRoutine()
    {
        while (true)
        {
            // Generate random target scale values within the specified range for each axis
            Vector3 targetScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
            );

            // Smoothly interpolate from the current scale to the target scale
            Vector3 initialScale = transform.localScale;
            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final scale is set
            transform.localScale = targetScale;

            // Wait for a short period before starting the next scaling transition
            yield return new WaitForSeconds(1.0f);
        }
    }
}
