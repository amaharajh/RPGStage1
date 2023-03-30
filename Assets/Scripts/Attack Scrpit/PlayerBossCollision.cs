using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossCollision : MonoBehaviour
{
    
     // Reference to the player's rigidbody component
    private Rigidbody2D playerRigidbody;

    // Set up reference to player rigidbody component
    private void Start () {
        playerRigidbody = GetComponent<Rigidbody2D> ();
    }

    // Detect collisions with enemies
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Boss")) {
            // Get the enemy's rigidbody component
            Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Get the direction of the collision
            Vector2 direction = collision.contacts[0].point - (Vector2)transform.position;

            // Disable collision between the player and enemy in the direction of the collision
            Physics2D.IgnoreCollision(playerRigidbody.GetComponent<Collider2D>(), enemyRigidbody.GetComponent<Collider2D>(), true);
            Invoke("EnableCollision", 0.1f);
        }
    }

    // Re-enable collision between the player and enemy after a short delay
    private void EnableCollision() {
        Physics2D.IgnoreCollision(playerRigidbody.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
}






