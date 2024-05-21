using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Optionally, add checks for specific collision tags if needed, e.g.:
        // if (collision.collider.tag == "Enemy")
        Destroy(gameObject); // Destroy this projectile
    }
}
