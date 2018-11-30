using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour {

    #region private fields
    private Rigidbody2D rb;
    private float countdownTime;
    
    private bool humanInOuterQuad1;
    private bool humanInOuterQuad2;
    private bool humanInOuterQuad3;
    private bool humanInOuterQuad4;
    private bool[] humanOuterQuads;

    private bool humanInInnerQuad1;
    private bool humanInInnerQuad2;
    private bool humanInInnerQuad3;
    private bool humanInInnerQuad4;
    private bool[] humanInnerQuads;

    private bool ratInQuad1;
    private bool ratInQuad2;
    private bool ratInQuad3;
    private bool ratInQuad4;
    private bool[] ratQuads;

    private List<bool> trueHumanOuterQuads = new List<bool>();
    private List<bool> trueHumanInnerQuads = new List<bool>();
    private List<bool> trueRatQuads = new List<bool>();

    private bool humansInOuterCircle;
    private bool humansInInnerCircle;
    private bool ratsInCircle;

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

    private float minXFloat;
    private float maxXFloat;
    private float minYFloat;
    private float maxYFloat;

    private System.Random rnd = new System.Random();
    #endregion

    #region serialized fields
    [SerializeField]
    private float time;
    [SerializeField]
    private float minXRandomFloat;
    [SerializeField]
    private float maxXRandomFloat;
    [SerializeField]
    private float minYRandomFloat;
    [SerializeField]
    private float maxYRandomFloat;
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
    #endregion

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        countdownTime = 0;

        minXFloat = minXRandomFloat;
        maxXFloat = maxXRandomFloat;
        minYFloat = minYRandomFloat;
        maxYFloat = maxYRandomFloat;

        humanOuterQuads = new bool[] {humanInOuterQuad1, humanInOuterQuad2, humanInOuterQuad3, humanInOuterQuad4};
        humanInnerQuads = new bool[] {humanInInnerQuad1, humanInInnerQuad2, humanInInnerQuad3, humanInInnerQuad4};
        ratQuads = new bool[] {ratInQuad1, ratInQuad2, ratInQuad3, ratInQuad4};
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Debug.Log(trueHumanInnerQuads.Count);
        //Debug.Log(trueHumanOuterQuads.Count);
        //Debug.Log(trueRatQuads.Count);

        
        ChangeMovementAfterCountdown();
    }

    private void ChangeMovementAfterCountdown()
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
        ChangeMovementVector();
        rb.velocity = RandomVector(minXFloat, maxXFloat, minYFloat, maxYFloat);
    }

    private void ClearLists()
    {
        trueHumanInnerQuads.Clear();
        trueHumanOuterQuads.Clear();
        trueRatQuads.Clear();
    }

    private void ChangeMovementVector()
    {
        colliderCheck();
        populateTrueBoolLists();

        if (humansInInnerCircle)
        {
            populateTrueBoolLists();
            InnerCircleMovement();
            Debug.Log("Inner");
        }
        else if (humansInOuterCircle)
        {
            populateTrueBoolLists();
            outerCircleMovement();
            Debug.Log("Outer");
        }
        else if (ratsInCircle)
        {
            populateTrueBoolLists();
            ratCircleMovement();
            Debug.Log("Rat");
        }
        else
        {
            Debug.Log("None");

            minXFloat = minXRandomFloat;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = maxYRandomFloat;
        }
    }

    private void populateTrueBoolLists()
    {
        foreach (bool outer in humanOuterQuads)
        {
            if (outer == true)
            {
                trueHumanOuterQuads.Add(outer);
            }
        }
        foreach (bool inner in humanInnerQuads)
        {
            if (inner == true)
            {
                trueHumanInnerQuads.Add(inner);
            }
        }
        foreach (bool rat in ratQuads)
        {
            if (rat == true)
            {
                trueRatQuads.Add(rat);
            }
        }
    }

    private void InnerCircleMovement()
    {
        int randomQuad;
        if (trueHumanInnerQuads.Count > 1)
        {
            randomQuad = rnd.Next(0, trueHumanInnerQuads.Count - 1);
        }
        else
        {
            randomQuad = 0;
        }

        if (trueHumanInnerQuads[randomQuad] == humanInInnerQuad1)
        {
            minXFloat = minXRandomFloat;
            maxXFloat = 0;
            minYFloat = minYRandomFloat;
            maxYFloat = 0;
        }
        else if (trueHumanInnerQuads[randomQuad] == humanInInnerQuad2)
        {
            
            minXFloat = 0;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = 0;
        }
        else if (trueHumanInnerQuads[randomQuad] == humanInInnerQuad3)
        {
            minXFloat = 0;
            maxXFloat = maxXRandomFloat;
            minYFloat = 0;
            maxYFloat = maxYRandomFloat;
        }
        else if (trueHumanInnerQuads[randomQuad] == humanInInnerQuad4)
        {
            minXFloat = minXRandomFloat;
            maxXFloat = 0;
            minYFloat = 0;
            maxYFloat = maxYRandomFloat;
        }
        else
        {
            minXFloat = minXRandomFloat;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = maxYRandomFloat;
        }

        ClearLists();
    }

    private void outerCircleMovement()
    {
        int randomQuad;
        if (trueHumanOuterQuads.Count > 1)
        {
            randomQuad = rnd.Next(0, trueHumanOuterQuads.Count - 1);
        }
        else
        {
            randomQuad = 0;
        }

        if (trueHumanOuterQuads[randomQuad] == humanInOuterQuad1)
        {
            minXFloat = 0;
            maxXFloat = maxXRandomFloat;
            minYFloat = 0;
            maxYFloat = maxYRandomFloat;
        }
        else if (trueHumanOuterQuads[randomQuad] == humanInOuterQuad2)
        {
            minXFloat = minXRandomFloat;
            maxXFloat = 0;
            minYFloat = 0;
            maxYFloat = maxYRandomFloat;
        }
        else if (trueHumanOuterQuads[randomQuad] == humanInOuterQuad3)
        {
            minXFloat = minXRandomFloat;
            maxXFloat = 0;
            minYFloat = minYRandomFloat;
            maxYFloat = 0;
        }
        else if (trueHumanOuterQuads[randomQuad] == humanInOuterQuad4)
        {
            minXFloat = 0;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = 0;
        }
        else
        {
            minXFloat = minXRandomFloat;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = maxYRandomFloat;
        }

        ClearLists();
    }

    private void ratCircleMovement()
    {
        int randomQuad;
        if (trueRatQuads.Count > 1)
        {
            randomQuad = rnd.Next(0, trueRatQuads.Count - 1);
        }
        else
        {
            randomQuad = 0;
        }

        if (trueRatQuads[randomQuad] == ratInQuad1)
        {
            minXFloat = 0;
            maxXFloat = maxXRandomFloat;
            minYFloat = 0;
            maxYFloat = maxYRandomFloat;
        }
        else if (trueRatQuads[randomQuad] == ratInQuad2)
        {
            minXFloat = minXRandomFloat;
            maxXFloat = 0;
            minYFloat = 0;
            maxYFloat = maxYRandomFloat;
        }
        else if (trueRatQuads[randomQuad] == ratInQuad3)
        {
            minXFloat = minXRandomFloat;
            maxXFloat = 0;
            minYFloat = minYRandomFloat;
            maxYFloat = 0;
        }
        else if (trueRatQuads[randomQuad] == ratInQuad4)
        {
            minXFloat = 0;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = 0;
        }
        else
        {
            minXFloat = minXRandomFloat;
            maxXFloat = maxXRandomFloat;
            minYFloat = minYRandomFloat;
            maxYFloat = maxYRandomFloat;
        }

        ClearLists();
    }

    private Vector2 RandomVector(float xMin, float xMax, float yMin, float yMax)
    {
        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);
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

        ratInQuad1 = outerQuad1DetectTrigger.OverlapCollider(ratContactFilter, quad1RatHitDetectionResults) > 1;
        ratInQuad2 = outerQuad2DetectTrigger.OverlapCollider(ratContactFilter, quad2RatHitDetectionResults) > 1;
        ratInQuad3 = outerQuad3DetectTrigger.OverlapCollider(ratContactFilter, quad3RatHitDetectionResults) > 1;
        ratInQuad4 = outerQuad4DetectTrigger.OverlapCollider(ratContactFilter, quad4RatHitDetectionResults) > 1;

        humansInOuterCircle = (humanInOuterQuad1 || humanInOuterQuad2 || humanInOuterQuad3 || humanInOuterQuad4);
        humansInInnerCircle = (humanInInnerQuad1 || humanInInnerQuad2 || humanInInnerQuad3 || humanInInnerQuad4);
        ratsInCircle = (ratInQuad1 || ratInQuad2 || ratInQuad3 || ratInQuad4);
    }
}
