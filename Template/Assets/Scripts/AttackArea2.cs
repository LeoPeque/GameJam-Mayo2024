using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea2 : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Player1>().Energia -= damage;
            collider.GetComponent<Animator>().Play("apple_Hurt");
        }
    }
}