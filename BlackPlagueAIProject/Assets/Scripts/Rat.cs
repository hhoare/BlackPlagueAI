using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour {

    private Rigidbody2D rb;
    private float countdownTime;

    private bool humanInOuterQuad1;
    private bool humanInOuterQuad2;
    private bool humanInOuterQuad3;
    private bool humanInOuterQuad4;

    private bool humanInInnerQuad1;
    private bool humanInInnerQuad2;
    private bool humanInInnerQuad3;
    private bool humanInInnerQuad4;

    private bool ratInQuad1;
    private bool ratInQuad2;
    private bool ratInQuad3;
    private bool ratInQuad4;

    private bool humanInOuterCircle;
    private bool humanInInnerCircle;
    private bool ratInCircle;

    private Collider2D[] innerQuad1HitDetectionResults = new Collider2D[50];
    private Collider2D[] innerQuad2HitDetectionResults = new Collider2D[50];
    private Collider2D[] innerQuad3HitDetectionResults = new Collider2D[50];
    private Collider2D[] innerQuad4HitDetectionResults = new Collider2D[50];

    private Collider2D[] outerQuad1HumanHitDetectionResults = new Collider2D[50];
    private Collider2D[] outerQuad2HumanHitDetectionResults = new Collider2D[50];
    private Collider2D[] outerQuad3HumanHitDetectionResults = new Collider2D[50];
    private Collider2D[] outerQuad4HumanHitDetectionResults = new Collider2D[50];

    private Collider2D[] quad1RatHitDetectionResults = new Collider2D[50];
    private Collider2D[] quad2RatHitDetectionResults = new Collider2D[50];
    private Collider2D[] quad3RatHitDetectionResults = new Collider2D[50];
    private Collider2D[] quad4RatHitDetectionResults = new Collider2D[50];

    [SerializeField]
    private float time;
    [SerializeField]
    private float minRandomFloat;
    [SerializeField]
    private float maxRandomFloat;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Collider2D outerQuad1DetectTrigger;
    [SerializeField]
    private Collider2D outerQuad2DetectTrigger;
    [SerializeField]
    private Collider2D outerQuad3DetectTrigger;
    [SerializeField]
    private Collider2D outerQuad4DetectTrigger;

    [SerializeField]
    private Collider2D innerQuad1DetectTrigger;
    [SerializeField]
    private Collider2D innerQuad2DetectTrigger;
    [SerializeField]
    private Collider2D innerQuad3DetectTrigger;
    [SerializeField]
    private Collider2D innerQuad4DetectTrigger;

    [SerializeField]
    private ContactFilter2D humanContactFilter;
    [SerializeField]
    private ContactFilter2D ratContactFilter;

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

    private void colliderCheck()
    {
        humanInOuterQuad1 = outerQuad1DetectTrigger.OverlapCollider(humanContactFilter, outerQuad1HumanHitDetectionResults) > 0;
        humanInOuterQuad2 = outerQuad2DetectTrigger.OverlapCollider(humanContactFilter, outerQuad2HumanHitDetectionResults) > 0;
        humanInOuterQuad3 = outerQuad3DetectTrigger.OverlapCollider(humanContactFilter, outerQuad3HumanHitDetectionResults) > 0;
        humanInOuterQuad4 = outerQuad4DetectTrigger.OverlapCollider(humanContactFilter, outerQuad4HumanHitDetectionResults) > 0;

        humanInInnerQuad1 = innerQuad1DetectTrigger.OverlapCollider(humanContactFilter, innerQuad1HitDetectionResults) > 0;
        humanInInnerQuad2 = innerQuad2DetectTrigger.OverlapCollider(humanContactFilter, innerQuad2HitDetectionResults) > 0;
        humanInInnerQuad3 = innerQuad3DetectTrigger.OverlapCollider(humanContactFilter, innerQuad3HitDetectionResults) > 0;
        humanInInnerQuad4 = innerQuad4DetectTrigger.OverlapCollider(humanContactFilter, innerQuad4HitDetectionResults) > 0;

        ratInQuad1 = outerQuad1DetectTrigger.OverlapCollider(ratContactFilter, quad1RatHitDetectionResults) > 0;
        ratInQuad2 = outerQuad2DetectTrigger.OverlapCollider(ratContactFilter, quad2RatHitDetectionResults) > 0;
        ratInQuad3 = outerQuad3DetectTrigger.OverlapCollider(ratContactFilter, quad3RatHitDetectionResults) > 0;
        ratInQuad4 = outerQuad4DetectTrigger.OverlapCollider(ratContactFilter, quad4RatHitDetectionResults) > 0;

        humanInOuterCircle = (humanInOuterQuad1 || humanInOuterQuad2 || humanInOuterQuad3 || humanInOuterQuad4);
        humanInInnerCircle = (humanInInnerQuad1 || humanInInnerQuad2 || humanInInnerQuad3 || humanInInnerQuad4);
        ratInCircle = (ratInQuad1 || ratInQuad2 || ratInQuad3 || ratInQuad4);
    }
}
