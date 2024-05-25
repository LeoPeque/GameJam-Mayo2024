using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandia : MonoBehaviour{

    Animator anim;
    public AnimationClip[] acciones;
    public Rigidbody2D semilla;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RandomAction(){
        string animacion = acciones[Random.Range(0,acciones.Length)].name;
        anim.Play(animacion);
    }

    public void Escupir(){
        Rigidbody2D semillas = Instantiate(semilla,transform.GetChild(0).position,Quaternion.Euler(new Vector3(0,0,1))) as Rigidbody2D;
        semillas.AddForce((transform.localScale.x * -transform.right)*400f);
    }
}
