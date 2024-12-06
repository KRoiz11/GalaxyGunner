using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
    public float bulletDamage = 20f;

    private void OnCollisionEnter(Collision collision)
    {   //checks if bullet collides with enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //gets the enemy health component
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bulletDamage);
            }
            //destroys the bullet
            Destroy(gameObject);
        }

        //checks if bullet collides with ground
        else if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
