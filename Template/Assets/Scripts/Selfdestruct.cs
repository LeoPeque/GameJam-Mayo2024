using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour{
    float tiempoVida = 5f;

    // Update is called once per frame
    void Update()
    {
        tiempoVida -= Time.deltaTime;
        if(tiempoVida <= 0){
            Destroy(gameObject);
        }
    }
}
