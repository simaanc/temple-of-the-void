using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth; 
    public bool isBoss;
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
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // Update health bar at start
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
        if (isBoss) // Check if the enemy is a boss
        {
            SceneManager.LoadScene("Winning Scene");
        }
        Destroy(gameObject); // Destroy the enemy object
    }
}