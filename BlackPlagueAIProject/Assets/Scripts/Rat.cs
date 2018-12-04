using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField]
    float ratDetectRadius;
    [SerializeField]
    float humanDetectRadius;
    [SerializeField]
    float infectedRadius;
    [SerializeField]
    float speed;
    [SerializeField]
    float lifeSpan;
    [SerializeField]
    float spawnRate;
    [SerializeField]
    float directionUpdateTime; //How long before the direction is updated
    private float countDownTime; //Used to copy directionUpdateTime for decrementing
    private Rigidbody2D rb;
    [SerializeField]
    float xMin = -1;
    [SerializeField]
    float xMax = 1;
    [SerializeField]
    float yMin = -1;
    [SerializeField]
    float yMax = 1;

    private int directionCounter = 5; //Used to make human more likely to travel in same direction for however many turns
    private float ratVariable = 0.75f; //Used to make human less likely to move towards rats in detection radius
    private float humanVariable = 0.5f; //Used to make human more likely to move towards other humans in detection radius
    private float infectedVariable = 0.8f; //Used to make human less likely to move towards infected in detection radius
    [SerializeField]
    ContactFilter2D uninfectedContactFilter;
    [SerializeField]
    ContactFilter2D infectedContactFilter;
    [SerializeField]
    ContactFilter2D ratContactFilter;

    private bool upCloseDetected;
    private bool downCloseDetected;
    private bool leftCloseDetected;
    private bool rightCloseDetected;

    private bool upHumanDetected;
    private bool downHumanDetected;
    private bool leftHumanDetected;
    private bool rightHumanDetected;

    private bool upInfectedDetected;
    private bool downInfectedDetected;
    private bool leftInfectedDetected;
    private bool rightInfectedDetected;

    private Collider2D[] upCloseDetection = new Collider2D[50];
    private Collider2D[] downCloseDetection = new Collider2D[50];
    private Collider2D[] leftCloseDetection = new Collider2D[50];
    private Collider2D[] rightCloseDetection = new Collider2D[50];

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
    Collider2D upClose;
    [SerializeField]
    Collider2D downClose;
    [SerializeField]
    Collider2D leftClose;
    [SerializeField]
    Collider2D rightClose;
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
        if (countDownTime <= 0)
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

       
        if (upCloseDetected == true)
        {
            tempYMax = 0.4f;
        }
        if (downCloseDetected == true)
        {
            tempYMin = -0.4f;
        }
        if (leftCloseDetected == true)
        {
            tempXMin = -0.4f;
        }
        if (rightCloseDetected == true)
        {
            tempXMax = 0.4f;
        }
        


        if (upHumanDetected == true||upInfectedDetected==true)
        {
            tempYMax = 2f;
        }
        if (downHumanDetected == true || upInfectedDetected == true)
        {
            tempYMin = -2f;
        }
        if (leftHumanDetected == true || upInfectedDetected == true)
        {
            tempXMin = -2;
        }
        if (rightHumanDetected == true || upInfectedDetected == true)
        {
            tempXMax = 2f;
        }


        rb.velocity = RandomVector(tempXMin, tempXMax, tempYMin, tempYMax); //Change velocity based on a random vector with the mins and maxes

    }

    private Vector2 RandomVector(float xMin, float xMax, float yMin, float yMax) //vector for movement
    {
        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);
        return new Vector2(x * speed, y * speed);
    }

    private void ColliderCheck() //Used to check whether or not there are rats/uninfected/infected in range
    {
       

        //   Bool     Collider2D   Overlapping   ContactFilter     Array        Amount
        upCloseDetected = upClose.OverlapCollider(ratContactFilter, upCloseDetection) > 1;
        downCloseDetected = downClose.OverlapCollider(ratContactFilter, downCloseDetection) > 1;
        leftCloseDetected = leftClose.OverlapCollider(ratContactFilter, leftCloseDetection) > 1;
        rightCloseDetected = rightClose.OverlapCollider(ratContactFilter, rightCloseDetection) > 1;

        upHumanDetected = upHuman.OverlapCollider(uninfectedContactFilter, upHumanDetection) > 0;
        downHumanDetected = downHuman.OverlapCollider(uninfectedContactFilter, downHumanDetection) > 0;
        leftHumanDetected = leftHuman.OverlapCollider(uninfectedContactFilter, leftHumanDetection) > 0;
        rightHumanDetected = rightHuman.OverlapCollider(uninfectedContactFilter, rightHumanDetection) > 0;

        //Uses the same trigger as uninfected
        upInfectedDetected = upHuman.OverlapCollider(infectedContactFilter, upInfectedDetection) > 0;
        downInfectedDetected = downHuman.OverlapCollider(infectedContactFilter, downInfectedDetection) > 0;
        leftInfectedDetected = leftHuman.OverlapCollider(infectedContactFilter, leftInfectedDetection) > 0;
        rightInfectedDetected = rightHuman.OverlapCollider(infectedContactFilter, rightInfectedDetection) > 0;
    }

}

