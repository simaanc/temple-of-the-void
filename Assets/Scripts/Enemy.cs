using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3; // The enemy's maximum health
    public int currentHealth; // Make currentHealth public

    Rigidbody2D rb;

    [SerializeField] FloatingHealthBar healthBar;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth; // Initialize currentHealth
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Reduce health by the damage amount
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
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
