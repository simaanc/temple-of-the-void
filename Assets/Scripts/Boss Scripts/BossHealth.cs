using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int bossMaxHealth = 100;
    public int bossCurrentHealth;

    void Start()
    {
        bossCurrentHealth = bossMaxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage(projectile.damage);
        }
    }

    void TakeDamage(int damage)
    {
        bossCurrentHealth -= damage;
        if (bossCurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
    }
}
