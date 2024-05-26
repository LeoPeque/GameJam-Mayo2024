using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUMovement : MonoBehaviour
{
    Rigidbody2D cuerpo;
    public float Fuerza = 10f;
    public float customUpdateInterval = 0.7f; // Desired interval in seconds
    private float customUpdateTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y > 8 || transform.position.y < -6){
            if(transform.position.y < 0){
                cuerpo.AddForce(new Vector3(0,Fuerza,0));
            }
            else{
                cuerpo.AddForce(new Vector3(0,-Fuerza,0));
            }
        }
        else{
            if(transform.position.x < -18 || transform.position.x > 18){
                if(transform.position.x < 0){
                    cuerpo.AddForce(new Vector3(Fuerza, 0,0));
                }
                else{
                    cuerpo.AddForce(new Vector3(-Fuerza, 0,0));

                }
            }
            else{
                cuerpo.AddForce(new Vector3(Random.Range(-Fuerza, Fuerza), Random.Range(-Fuerza, Fuerza)));
            }
        }

        customUpdateTimer += Time.fixedDeltaTime;
        if (customUpdateTimer >= customUpdateInterval)
        {
            customUpdateTimer = 0f;
        }
    }
}
