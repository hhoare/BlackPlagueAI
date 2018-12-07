using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
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

    private int directionCounter = 5; //Used to make human more likely to travel in same direction for however many turns

    [SerializeField][Tooltip("Higher number = more likely to move towards rats in detection radius")]
    private float ratVariable; //Used to make human less likely to move towards rats in detection radius
    [SerializeField][Tooltip("Higher number = more likely to move towards humans in outer detection radius")]
    private float humanOuterVariable; //Used to make human more likely to move towards other humans in detection radius
    [SerializeField][Tooltip("Lower number = more likely to move away from humans in inner detection radius")]
    private float humanInnerVariable; //Used to make human less likely to move towards infected in detection radius

    [SerializeField]
    private ContactFilter2D uninfectedContactFilter;
    [SerializeField]
    private ContactFilter2D infectedContactFilter;
    [SerializeField]
    private ContactFilter2D ratContactFilter;

    private bool upCloseDetected;
    private bool downCloseDetected;
    private bool leftCloseDetected;
    private bool rightCloseDetected;

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
    private Collider2D upClose;
    [SerializeField]
    private Collider2D downClose;
    [SerializeField]
    private Collider2D leftClose;
    [SerializeField]
    private Collider2D rightClose;
    [SerializeField]
    private Collider2D upHuman;
    [SerializeField]
    private Collider2D downHuman;
    [SerializeField]
    private Collider2D leftHuman;
    [SerializeField]
    private Collider2D rightHuman;


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

        if (upHumanDetected == true||upInfectedDetected==true)
        {
            tempYMax = humanOuterVariable;
        }
        if (downHumanDetected == true || upInfectedDetected == true)
        {
            tempYMin = -humanOuterVariable;
        }
        if (leftHumanDetected == true || upInfectedDetected == true)
        {
            tempXMin = -humanOuterVariable;
        }
        if (rightHumanDetected == true || upInfectedDetected == true)
        {
            tempXMax = humanOuterVariable;
        }


        if (upCloseDetected == true)
        {
            tempYMax = humanInnerVariable;
        }
        if (downCloseDetected == true)
        {
            tempYMin = -humanInnerVariable;
        }
        if (leftCloseDetected == true)
        {
            tempXMin = -humanInnerVariable;
        }
        if (rightCloseDetected == true)
        {
            tempXMax = humanInnerVariable;
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
       
        //If human is too close
        //   Bool     Collider2D   Overlapping   ContactFilter     Array        Amount
        upCloseDetected = upClose.OverlapCollider(uninfectedContactFilter, upCloseDetection) > 0;
        downCloseDetected = downClose.OverlapCollider(uninfectedContactFilter, downCloseDetection) > 0;
        leftCloseDetected = leftClose.OverlapCollider(uninfectedContactFilter, leftCloseDetection) > 0;
        rightCloseDetected = rightClose.OverlapCollider(uninfectedContactFilter, rightCloseDetection) > 0;

        //If infected is too close
        //Uses the same trigger as uninfected
        upInfectedDetected = upClose.OverlapCollider(infectedContactFilter, upInfectedDetection) > 0;
        downInfectedDetected = downClose.OverlapCollider(infectedContactFilter, downInfectedDetection) > 0;
        leftInfectedDetected = leftClose.OverlapCollider(infectedContactFilter, leftInfectedDetection) > 0;
        rightInfectedDetected = rightClose.OverlapCollider(infectedContactFilter, rightInfectedDetection) > 0;

        //If uninfected is within outer range
        upHumanDetected = upHuman.OverlapCollider(uninfectedContactFilter, upHumanDetection) > 0;
        downHumanDetected = downHuman.OverlapCollider(uninfectedContactFilter, downHumanDetection) > 0;
        leftHumanDetected = leftHuman.OverlapCollider(uninfectedContactFilter, leftHumanDetection) > 0;
        rightHumanDetected = rightHuman.OverlapCollider(uninfectedContactFilter, rightHumanDetection) > 0;

        //If uninfected is within outer range
        upRatDetected = upHuman.OverlapCollider(ratContactFilter, upHumanDetection) > 1;
        downRatDetected = downHuman.OverlapCollider(ratContactFilter, downHumanDetection) > 1;
        leftRatDetected = leftHuman.OverlapCollider(ratContactFilter, leftHumanDetection) > 1;
        rightRatDetected = rightHuman.OverlapCollider(ratContactFilter, rightHumanDetection) > 1;

    }

    public void ChangeRatsSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}

