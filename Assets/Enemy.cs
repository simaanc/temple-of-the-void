using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3; // The enemy's maximum health
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destroy the enemy object
    }
}