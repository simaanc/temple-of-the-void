using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    
    Rigidbody2D rb;

    [SerializeField] FloatingHealthBar healthBar;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health,maxHealth);

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
