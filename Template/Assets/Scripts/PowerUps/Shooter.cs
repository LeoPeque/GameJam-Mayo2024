using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject aimPrefab;
    public GameObject capsulePrefab;
    public GameObject bulletPrefab;
    public float speed = 3.0f;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private GameObject aimInstance;
    private GameObject capsuleInstance;
    private bool isActive = false;
    private float nextFireTime = 2f;
    private int bulletCount = 0; // Counter for bullets
    private const int maxBullets = 3; // Maximum number of bullets allowed
    public Vector3 safePosition = new Vector3(0, -22, 0);
    GameObject agresor;
    private Animator rifleAnim;
    private Animator aimAnim;
    public string winnerName = "";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            agresor = collision.gameObject;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            if (agresor.GetComponent<Player1>() != null)
            {
                agresor.GetComponent<Player1>().powered = true;
            }
            else
            {
                agresor.GetComponent<Player2>().powered = true;
            }
        }
    }
    void Update()
    {
        if (isActive)
        {
            UpdatePositions();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime && bulletCount < maxBullets)
            {
                ShootBullet();
                rifleAnim.SetTrigger("Shooter");
                nextFireTime = Time.time + fireRate;
            }
            if (bulletCount == 3)
            {
                rifleAnim.SetTrigger("Disappear");
                aimAnim.SetTrigger("DisappearAim");
                StartCoroutine(DestroyAfterDelay(1.5f)); // Llama a la coroutine con un retraso de 2 segundos
            }
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(capsuleInstance);
        Destroy(aimInstance);
    }


    public void Initialize(GameObject victim)
    {

        if (victim.name == "Player1")
        {
            GameObject Player2 = GameObject.Find("Player2");
            Player2.transform.position = new Vector3(-30, 18, 0); 
            winnerName = "Player2";
        }
        else if (victim.name == "Player2")
        {
            GameObject Player1 = GameObject.Find("Player1");
            Player1.transform.position = new Vector3(-30, -18, 0);
            winnerName = "Player1";
        }
        
        if (aimPrefab != null)
        {
            aimInstance = Instantiate(aimPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            aimAnim = aimInstance.GetComponent<Animator>();
            Destroy(aimInstance, 10.0f);
        }
        if (capsulePrefab != null)
        {
            capsuleInstance = Instantiate(capsulePrefab, new Vector3(-13, 5, 0), Quaternion.identity);
            rifleAnim = capsuleInstance.GetComponent<Animator>();
            Destroy(capsuleInstance, 10.0f);
        }
        ActivateShooter();
    }

    public void ActivateShooter()
    {
        isActive = true;
    }

    public void DeactivateShooter()
    {
        isActive = false;
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
        if (capsuleInstance != null && bulletPrefab != null && bulletCount < maxBullets)
        {
            Vector3 currentPosition = capsuleInstance.transform.position;
            Vector3 bulletPosition = new Vector3(currentPosition.x, currentPosition.y - 0.3f, currentPosition.z);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, capsuleInstance.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(capsuleInstance.transform.right * bulletSpeed);
            }
            bulletCount++; // Increment the bullet counter
        }
    }

    public void ResetBulletCount()
    {
        bulletCount = 0; // Reset the bullet counter to allow firing again
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Shooter : MonoBehaviour
// {
//     public GameObject aimPrefab;
//     public GameObject capsulePrefab;
//     public GameObject bulletPrefab;
//     public float speed = 3.0f;
//     public float bulletSpeed = 10f;
//     public float fireRate = 0.5f;
//     private GameObject aimInstance;
//     private GameObject capsuleInstance;
//     private bool isActive = false;
//     private float nextFireTime = 5f;
//     private int bulletCount = 0; // Counter for bullets
//     private const int maxBullets = 3; // Maximum number of bullets allowed
//     public Vector3 safePosition = new Vector3(0, -22, 0);
//     GameObject agresor;
//     private Animator rifleAnim;
//     public string winnerName = "";

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             agresor = collision.gameObject;
//             GetComponent<Renderer>().enabled = false;
//             GetComponent<Collider2D>().enabled = false;
//             if (agresor.GetComponent<Player1>() != null)
//             {
//                 agresor.GetComponent<Player1>().powered = true;
//             }
//             else
//             {
//                 agresor.GetComponent<Player2>().powered = true;
//             }
//         }
//     }

//     void Update()
//     {
//         if (isActive)
//         {
//             UpdatePositions();
//             if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime && bulletCount < maxBullets)
//             {
//                 ShootBullet();
//                 rifleAnim.SetTrigger("Shooter");
//                 nextFireTime = Time.time + fireRate;
//             }
//             if (bulletCount == 3)
//             {
//                 Destroy(capsuleInstance);
//                 Destroy(aimInstance);
//                 UpdatePositionsPlayers();
//             }
//         }
//     }

//     public void UpdatePositionsPlayers()
//     {
//                 if (winnerName == "Player1")
//                 {
//                     GameObject Player1 = GameObject.Find("Player1");
//                     Player1.transform.position = new Vector3(-8, -3, 0); 
//                 }
//                 else if (winnerName == "Player2")
//                 {
//                     GameObject Player2 = GameObject.Find("Player2");
//                     Player2.transform.position = new Vector3(8, 3, 0);
//                 }
//     }
//     public void Initialize(GameObject victim)
//     {

//         if (victim.name == "Player1")
//         {
//             GameObject Player2 = GameObject.Find("Player2");
//             Player2.transform.position = new Vector3(-30, 18, 0); 
//             winnerName = "Player2";
//         }
//         else if (victim.name == "Player2")
//         {
//             GameObject Player1 = GameObject.Find("Player1");
//             Player1.transform.position = new Vector3(-30, -18, 0);
//             winnerName = "Player1";
//         }
        
//         if (aimPrefab != null)
//         {
//             aimInstance = Instantiate(aimPrefab, new Vector3(0, 0, 0), Quaternion.identity);
//             Destroy(aimInstance, 10.0f);
//         }
//         if (capsulePrefab != null)
//         {
//             capsuleInstance = Instantiate(capsulePrefab, new Vector3(-13, 5, 0), Quaternion.identity);
//             rifleAnim = capsuleInstance.GetComponent<Animator>();
//             Destroy(capsuleInstance, 10.0f);
//         }
//         ActivateShooter();
//     }

//     public void ActivateShooter()
//     {
//         isActive = true;
//     }

//     public void DeactivateShooter()
//     {
//         isActive = false;
//     }

//     private void UpdatePositions()
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

//     private void ShootBullet()
//     {
//         if (capsuleInstance != null && bulletPrefab != null && bulletCount < maxBullets)
//         {
//             Vector3 currentPosition = capsuleInstance.transform.position;
//             Vector3 bulletPosition = new Vector3(currentPosition.x, currentPosition.y - 0.3f, currentPosition.z);
//             GameObject bullet = Instantiate(bulletPrefab, bulletPosition, capsuleInstance.transform.rotation);
//             Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
//             if (rb != null)
//             {
//                 rb.AddForce(capsuleInstance.transform.right * bulletSpeed);
//             }
//             bulletCount++; // Increment the bullet counter
//         }
//     }

//     public void ResetBulletCount()
//     {
//         bulletCount = 0; // Reset the bullet counter to allow firing again
//     }
// }

