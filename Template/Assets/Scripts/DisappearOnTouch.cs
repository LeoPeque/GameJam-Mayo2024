using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Ensure the colliding object is the player
        {
            Shooter shooter = collision.gameObject.GetComponent<Shooter>();
            if (shooter != null)
            {
                shooter.InitializeInstances();  // Initialize or reinitialize the shooter
                shooter.ActivateShooter();      // Ensure shooter updates are active
            }

            Destroy(gameObject);  // Destroy the power-up object
        }
        else if (collision.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
