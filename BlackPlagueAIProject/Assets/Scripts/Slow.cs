using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour {

    [SerializeField]
    private float slowSpeed=1.15f; //How much the velocity is divided by. Higher number= Slower

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D name = collision.gameObject.GetComponent<Rigidbody2D>();
        name.velocity = name.velocity / 1.15f;
    }
}
