using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour {

    private Rigidbody2D rb;
    private float countdownTime;

    [SerializeField]
    private float time;
    [SerializeField]
    private float minRandomFloat;
    [SerializeField]
    private float maxRandomFloat;
    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        countdownTime = time;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        countdownTime -= Time.deltaTime;
        if (countdownTime <= 0)
        {
            Move();
            countdownTime = time;
        }
    }

    private void Move()
    {
        rb.velocity = RandomVector(minRandomFloat, maxRandomFloat);
    }

    private Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector2(x*speed, y*speed);
    }
}
