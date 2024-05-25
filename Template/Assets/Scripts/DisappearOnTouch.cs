using UnityEngine;

public class DisappearOnTouch : MonoBehaviour
{
    // private SpecialAbility specialAbility;

    // private void Start()
    // {
    //     // Try to get the SpecialAbility script attached to the same GameObject
    //     specialAbility = GetComponent<SpecialAbility>();
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))  // Ensure the colliding object is the player
        {
            // Check if this GameObject's tag is "Shooter"
            if (gameObject.tag == "PowerDown")
            {
                Debug.Log("Special Ability Activated!");
                // specialAbility.Activate();  // Activate the Special Ability
                // specialAbility.enabled = true; // Enable the script if needed
            }

            Destroy(gameObject);  // Destroy the object
        }
    }
}
