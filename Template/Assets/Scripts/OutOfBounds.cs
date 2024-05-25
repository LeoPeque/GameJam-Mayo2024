using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Vector2 safePosition2 = new Vector2(8.07f, -2.64f);  // Default safe position
    public Vector2 safePosition1 = new Vector2(-8.07f, -2.64f);  // Default safe position

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounds") && gameObject.name == "Player1")
        {
            // Change the GameObject's position to the safe position when it collides with bounds
            transform.position = safePosition1;
        }
        else if (collision.gameObject.CompareTag("Bounds") && gameObject.name == "Player2")
        {
            // Change the GameObject's position to the safe position when it collides with bounds
            transform.position = safePosition2;
        }
    }
}
