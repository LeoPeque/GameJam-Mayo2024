
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea2 : MonoBehaviour
{
    private int damage = 3;

    void OnTriggerEnter2D(Collider2D cllider)
    {
        if (cllider.tag == "Player")
        {
            cllider.GetComponent<Player1>().Energia -= damage;
            cllider.GetComponent<Animator>().Play("apple_Hurt");
        }
    }
}
