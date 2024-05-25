using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox1 : MonoBehaviour{
    public int damage;

    void OnTriggerEnter2D(Collider2D colisionador){
        if(colisionador.tag == "Player"){
            if(colisionador.name == "Player1"){
                colisionador.GetComponent<Player1>().Energia -= damage;
            }
            else{
                colisionador.GetComponent<Player2>().Energia -= damage;
            }
            colisionador.GetComponent<Animator>().Play("apple_Hurt");
        }
    }

}
