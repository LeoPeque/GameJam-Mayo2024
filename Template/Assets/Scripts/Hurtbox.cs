using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour{
    public int damage;

    void OnTriggerEnter2D(Collider2D colisionador){
        if(colisionador.tag == "Player"){
            colisionador.GetComponent<Player>().Energia -= damage;
            colisionador.GetComponent<Animator>().Play("apple_Hurt");
        }
    }

}
