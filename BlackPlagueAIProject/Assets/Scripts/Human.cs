using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
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

        Debug.Log(downInfectedDetected);
        Debug.Log(leftInfectedDetected);
        Debug.Log(rightInfectedDetected);
        //===========================================================================================================
        //If statements used to decrease chance of moving towards rat/infected, and increase chance of moving towards uninfected
        //===========================================================================================================
        
        if (upHumanDetected==true)
        {
            tempYMax = 2f;
        }
        if (downHumanDetected == true)
        {
            tempYMin = -2f;
        }
        if (leftHumanDetected == true)
        {
            tempXMin = -2;
        }
        if (rightHumanDetected == true)
        {
            tempXMax = 2f;
        }

        if (upRatDetected == true)
        {
            tempYMax = 0.4f;
        }
        if (downRatDetected == true)
        {
            tempYMin = -0.4f;
        }
        if (leftRatDetected == true)
        {
            tempXMin = -0.4f;
        }
        if (rightRatDetected == true)
        {
            tempXMax = 0.4f;
        }


        if (upInfectedDetected == true)
        {
            tempYMax = 0.2f;
        }
        if (downInfectedDetected == true)
        {
            tempYMin = -0.2f;
        }
        if (leftInfectedDetected == true)
        {
            tempXMin = -0.2f;
        }
        if (rightInfectedDetected == true)
        {
            tempXMax = 0.2f;
        }
        rb.velocity=RandomVector(tempXMin, tempXMax, tempYMin, tempYMax);

    }

    private Vector2 RandomVector(float xMin, float xMax, float yMin, float yMax)
    {
        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);
        return new Vector2(x*speed, y*speed);
    }

    private void ColliderCheck()
    {
        int humans = 0;
        int infected = 0;
        if (gameObject.layer == LayerMask.NameToLayer("Uninfected"))
        {
            humans = 1;
            infected = 0;
        }
        if(gameObject.layer==LayerMask.NameToLayer("Infected"))
        {
            infected = 1;
            humans = 0;
        }
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

}
