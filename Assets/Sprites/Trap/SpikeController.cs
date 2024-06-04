using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private bool isHarmful = false;

    // Function to be called when the spikes are fully up
    public void SetHarmful()
    {
        isHarmful = true;
    }

    // Function to be called when the spikes go down
    public void SetSafe()
    {
        isHarmful = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHarmful && collision.CompareTag("Player"))
        {
            // Handle damage to the player here
            Debug.Log("Player takes damage");
        }
    }
}