using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masher : MonoBehaviour
{
    public int Mashes1 = 0;
    public int Mashes2 = 0;
    public Rigidbody2D P1;
    public Rigidbody2D P2;
    public bool isActive = false;
    GameObject EInstance;
    GameObject LInstance;
    public GameObject EPrefab;
    public GameObject LPrefab;
    private float elapsedTime;
    GameObject agresor;
    bool change = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Ensure the colliding object is the player
        {
            agresor = collision.gameObject;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            if(agresor.GetComponent<Player1>() != null){
                agresor.GetComponent<Player1>().powered = true;
            }
            else{
                agresor.GetComponent<Player2>().powered = true;
                change = true;
            }
        }
    }

    public void Initialize(GameObject victim){
        P1 = agresor.GetComponent<Rigidbody2D>();
        P2 = victim.GetComponent<Rigidbody2D>();
        P1.constraints |= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        P2.constraints |= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        isActive = true;
        EInstance = Instantiate(EPrefab, new Vector3(P1.transform.position.x, P1.transform.position.y + 2f), Quaternion.identity);
        LInstance = Instantiate(LPrefab, new Vector3(P2.transform.position.x, P2.transform.position.y + 2f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if(Input.GetKeyDown(KeyCode.E)){
                Mashes1 += 1;
            }
            if(Input.GetKeyDown(KeyCode.L)){
                Mashes2 += 1;
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime > 5f)
            {
                Terminate();
            }
        }
    }

    void Terminate(){
        if(Mashes1 > Mashes2){
            P2.gameObject.GetComponent<Player2>().Energia -= 20;
        }
        P1.constraints &= ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY);
        P2.constraints &= ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY);
        isActive = false;
        Destroy(EInstance);
        Destroy(LInstance);
        Destroy(gameObject);
    }
}