using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3; // The enemy's maximum health
    public int currentHealth; // Make currentHealth public

    void Start()
    {
        currentHealth = maxHealth; // Initialize currentHealth
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Reduce health by the damage amount

        if (currentHealth <= 0)
        {
            Die(); // Call Die() if health is 0 or less
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destroy the enemy object
    }
}
