using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // The amount of damage the bullet deals

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        MonsterHealth monster = collision.gameObject.GetComponent<MonsterHealth>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }

        // Destroy the bullet on impact
        Destroy(gameObject);
    }
}