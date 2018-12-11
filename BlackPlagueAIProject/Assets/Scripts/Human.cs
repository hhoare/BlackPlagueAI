
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField]
    private float ratDetectRadius;
    [SerializeField]
    private float humanDetectRadius;
    [SerializeField]
    private float infectedRadius;
    
    [SerializeField]
    private float lifeSpan;
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    private float directionUpdateTime; //How long before the direction is updated
    private float countDownTime; //Used to copy directionUpdateTime for decrementing
    private Rigidbody2D rb;
    [SerializeField]
    private float xMin = -1;
    [SerializeField]
    private float xMax = 1;
    [SerializeField]
    private float yMin = -1;
    

    [SerializeField]
    private float yMax = 1;

    private static float speed = 5;

    //private int directionCounter = 5; //Used to make human more likely to travel in same direction for however many turns

    [SerializeField][Tooltip("Higher number = more likely to move towards rats in detection radius")]
    private float ratVariable;
    [SerializeField][Tooltip(("Higher number = more likely to move towards other humans in detection radius"))]
    private float humanVariable;
    [SerializeField][Tooltip("Higher number = more likely to move towards infected in detection radius")]
    private float infectedVariable;

    [SerializeField]
    ContactFilter2D uninfectedContactFilter;
    [SerializeField]
    ContactFilter2D infectedContactFilter;
    [SerializeField]
    ContactFilter2D ratContactFilter;

    private bool upRatDetected;
    private bool downRatDetected;
    private bool leftRatDetected;
    private bool rightRatDetected;

    private bool upHumanDetected;
    private bool downHumanDetected;
    private bool leftHumanDetected;
    private bool rightHumanDetected;

    private bool upInfectedDetected;
    private bool downInfectedDetected;
    private bool leftInfectedDetected;
    private bool rightInfectedDetected;

    private Collider2D[] upRatDetection = new Collider2D[50];
    private Collider2D[] downRatDetection = new Collider2D[50];
    private Collider2D[] leftRatDetection = new Collider2D[50];
    private Collider2D[] rightRatDetection = new Collider2D[50];

    private Collider2D[] upHumanDetection = new Collider2D[50];
    private Collider2D[] downHumanDetection = new Collider2D[50];
    private Collider2D[] leftHumanDetection = new Collider2D[50];
    private Collider2D[] rightHumanDetection = new Collider2D[50];

    private Collider2D[] upInfectedDetection = new Collider2D[50];
    private Collider2D[] downInfectedDetection = new Collider2D[50];
    private Collider2D[] leftInfectedDetection = new Collider2D[50];
    private Collider2D[] rightInfectedDetection = new Collider2D[50];

    //DetectionTriggers
    [SerializeField]
    Collider2D upRat;
    [SerializeField]
    Collider2D downRat;
    [SerializeField]
    Collider2D leftRat;
    [SerializeField]
    Collider2D rightRat;
    [SerializeField]
    Collider2D upHuman;
    [SerializeField]
    Collider2D downHuman;
    [SerializeField]
    Collider2D leftHuman;
    [SerializeField]
    Collider2D rightHuman;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        countDownTime = directionUpdateTime;
    }

    private void FixedUpdate()
    {
        countDownTime -= Time.deltaTime;
        if(countDownTime<=0)
        {
            Move();
            countDownTime = directionUpdateTime; //Resets countDownTime
        }
    }

    public void Move()
    {
        ColliderCheck();
        //Used to temporarily change x/y max/min

            float tempXMin = xMin;
            float tempXMax = xMax;
            float tempYMin = yMin;
            float tempYMax = yMax;
        //===========================================================================================================
        //If statements used to decrease chance of moving towards rat/infected, and increase chance of moving towards uninfected
        //===========================================================================================================
        
        if (upHumanDetected==true)
        {
            tempYMax = humanVariable;
        }
        if (downHumanDetected == true)
        {
            tempYMin = -humanVariable;
        }
        if (leftHumanDetected == true)
        {
            tempXMin = -humanVariable;
        }
        if (rightHumanDetected == true)
        {
            tempXMax = humanVariable;
        }

        if (upRatDetected == true)
        {
            tempYMax = ratVariable;
        }
        if (downRatDetected == true)
        {
            tempYMin = -ratVariable;
        }
        if (leftRatDetected == true)
        {
            tempXMin = -ratVariable;
        }
        if (rightRatDetected == true)
        {
            tempXMax = ratVariable;
        }


        if (upInfectedDetected == true)
        {
            tempYMax = infectedVariable;
        }
        if (downInfectedDetected == true)
        {
            tempYMin = -infectedVariable;
        }
        if (leftInfectedDetected == true)
        {
            tempXMin = -infectedVariable;
        }
        if (rightInfectedDetected == true)
        {
            tempXMax = infectedVariable;
        }

        rb.velocity=RandomVector(tempXMin, tempXMax, tempYMin, tempYMax); //Change velocity based on a random vector with the mins and maxes

    }

    private Vector2 RandomVector(float xMin, float xMax, float yMin, float yMax) //vector for movement
    {
        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);
        if(gameObject.layer==12) //If current object is infected
        {
            return new Vector2(x * (speed / 2), y * (speed / 2)); //Moves half as fast
        }
        else //Otherwise
        { 
            return new Vector2(x * speed, y * speed);
        }

    }

    private void ColliderCheck() //Used to check whether or not there are rats/uninfected/infected in range
    {
        int humans = 0; //Used to set the base amount of humans needed to effect
        int infected = 0; //Used to set the base amount of infected needed to effect

        if (gameObject.layer == LayerMask.NameToLayer("Uninfected")) //If the current human is uninfected
        {
            humans = 1; //There needs to be two uninfected in range, as the colliders see the current one
            infected = 0;
        }
        if(gameObject.layer==LayerMask.NameToLayer("Infected"))
        {
            infected = 1;
            humans = 0;
        }

        //   Bool     Collider2D   Overlapping   ContactFilter     Array        Amount
        upRatDetected = upRat.OverlapCollider(ratContactFilter, upRatDetection) > 0;
        downRatDetected = downRat.OverlapCollider(ratContactFilter, downRatDetection) > 0;
        leftRatDetected = leftRat.OverlapCollider(ratContactFilter, leftRatDetection) > 0;
        rightRatDetected = rightRat.OverlapCollider(ratContactFilter, rightRatDetection) > 0;

        upHumanDetected = upHuman.OverlapCollider(uninfectedContactFilter, upHumanDetection) > humans;
        downHumanDetected = downHuman.OverlapCollider(uninfectedContactFilter, downHumanDetection) > humans;
        leftHumanDetected = leftHuman.OverlapCollider(uninfectedContactFilter, leftHumanDetection) > humans;
        rightHumanDetected = rightHuman.OverlapCollider(uninfectedContactFilter, rightHumanDetection) > humans;

        //Uses the same trigger as uninfected
        upInfectedDetected = upHuman.OverlapCollider(infectedContactFilter, upInfectedDetection) > infected;
        downInfectedDetected = downHuman.OverlapCollider(infectedContactFilter, downInfectedDetection) > infected;
        leftInfectedDetected = leftHuman.OverlapCollider(infectedContactFilter, leftInfectedDetection) > infected;
        rightInfectedDetected = rightHuman.OverlapCollider(infectedContactFilter, rightInfectedDetection) > infected;
    }

    public void ChangeHumanVariable(float newHumanVariable)
    {
        humanVariable = newHumanVariable;
    }
    public void ChangeRatVariable(float newRatVariable)
    {
        ratVariable = newRatVariable;
    }
    public void ChangeInfectedVariable(float newInfectedVariable)
    {
        infectedVariable = newInfectedVariable;
    }
    public void ChangeHumansSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
