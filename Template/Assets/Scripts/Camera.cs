using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform jugador; 
    // Start is called before the first frame update
    void Start(){
        this.transform.position = new Vector3(jugador.position.x, jugador.position.y, -10);
    }

    // Update is called once per frame
    void Update(){
        this.transform.position = new Vector3(jugador.position.x, jugador.position.y, -10);

    }
}
