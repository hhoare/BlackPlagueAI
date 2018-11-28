using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour {

    private Rigidbody2D rb;

    [SerializeField]
    private float minRandomFloat;
    [SerializeField]
    private float maxRandomFloat;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rb.velocity = RandomVector(minRandomFloat, maxRandomFloat);
	}

    private Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector2();
    }
}
