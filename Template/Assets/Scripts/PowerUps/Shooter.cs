using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject aimPrefab; // Assign the Aim prefab in the inspector
    public GameObject capsulePrefab; // Assign the Capsule prefab in the inspector
    public GameObject bulletPrefab; // Assign the Bullet prefab in the inspector
    public float speed = 3.0f;
    public float bulletSpeed = 1f; // Bullet speed for better effect
    private GameObject aimInstance;
    private GameObject capsuleInstance;
    private bool isActive = false; // Controls whether updates and shooting are active

    void Update()
    {
        if (isActive)
        {
            UpdatePositions();
            if (Input.GetKeyDown(KeyCode.Space)) // Check for shooting input
            {
                ShootBullet();
            }
        }
    }

    public void InitializeInstances()
    {
        if (aimPrefab != null)
        {
            aimInstance = Instantiate(aimPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(aimInstance, 10.0f);
        }
        if (capsulePrefab != null)
        {
            capsuleInstance = Instantiate(capsulePrefab, new Vector3(-13, 5, 0), Quaternion.identity);
            Destroy(capsuleInstance, 10.0f);
        }
        ActivateShooter(); // Automatically activate when instances are initialized
    }

    public void ActivateShooter()
    {
        isActive = true; // Enable the shooter's functionality
    }

    public void DeactivateShooter()
    {
        isActive = false; // Disable the shooter's functionality
    }

    private void UpdatePositions()
    {
        if (aimInstance != null)
        {
            float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            aimInstance.transform.position += new Vector3(moveX, moveY, 0);
        }

        if (capsuleInstance != null && aimInstance != null)
        {
            Vector3 direction = aimInstance.transform.position - capsuleInstance.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            capsuleInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void ShootBullet()
    {
        if (capsuleInstance != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, capsuleInstance.transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(capsuleInstance.transform.right * bulletSpeed);
            }
        }
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Shooter : MonoBehaviour
// {
//     public GameObject aimPrefab; // Assign the Aim prefab in the inspector
//     public GameObject capsulePrefab; // Assign the Capsule prefab in the inspector
//     public float speed = 3.0f;
//     private GameObject aimInstance;
//     private GameObject capsuleInstance;

//     void Start()
//     {
//         if (aimPrefab != null)
//         {
//             aimInstance = Instantiate(aimPrefab, new Vector3(0, 0, 0), Quaternion.identity);
//             Destroy(aimInstance, 10.0f);
//         }
//         if (capsulePrefab != null)
//         {
//             capsuleInstance = Instantiate(capsulePrefab, new Vector3(-13, 5, 0), Quaternion.identity);
//             Destroy(capsuleInstance, 10.0f);
//         }
//     }

//     void Update()
//     {
//         if (aimInstance != null)
//         {
//             float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//             float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
//             aimInstance.transform.position += new Vector3(moveX, moveY, 0);
//         }

//         if (capsuleInstance != null && aimInstance != null)
//         {
//             Vector3 direction = aimInstance.transform.position - capsuleInstance.transform.position;
//             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//             capsuleInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//         }
//     }
// }
