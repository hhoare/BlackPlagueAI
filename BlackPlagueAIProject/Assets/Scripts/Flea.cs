using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flea : MonoBehaviour
{
    [SerializeField]
    private float ratDetectRadius;
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

    [SerializeField]
    [Tooltip("Higher number = more likely to move towards rats in detection radius")]
    private float ratVariable; //Used to make human less likely to move towards rats in detection radius

    [SerializeField]
    [Tooltip("Lower number = more likely to move away from rats in inner detection radius")]
    private float ratInnerVariable; //Used to make human less likely to move towards infected in detection radius


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


    private Collider2D[] upCloseDetection = new Collider2D[50];
    private Collider2D[] downCloseDetection = new Collider2D[50];
    private Collider2D[] leftCloseDetection = new Collider2D[50];
    private Collider2D[] rightCloseDetection = new Collider2D[50];

    private Collider2D[] upRatDetection = new Collider2D[50];
    private Collider2D[] downRatnDetection = new Collider2D[50];
    private Collider2D[] leftRatDetection = new Collider2D[50];
    private Collider2D[] rightRatDetection = new Collider2D[50];


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
    private Collider2D upRat;
    [SerializeField]
    private Collider2D downRat;
    [SerializeField]
    private Collider2D leftRat;
    [SerializeField]
    private Collider2D rightRat;

    private float timeSinceInstantiation;
    private static Population population = new Population();


    private bool isAttached = false;


    private void Awake()
    {
        timeSinceInstantiation = Time.time;
        rb = GetComponent<Rigidbody2D>();
        countDownTime = directionUpdateTime;
    }

    private void FixedUpdate()
    {
        if (Time.time >= lifeSpan + timeSinceInstantiation)
        {
            population.DecrementRats();
            Destroy(gameObject);
        }

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




        if (upCloseDetected == true)
        {
            tempYMax = ratInnerVariable;
            if (Random.value < 0.75)
            {
            //    isAttached = true;
           //     this.transform.position = upCloseDetection.
            }
        }
        if (downCloseDetected == true)
        {
            tempYMin = -ratInnerVariable;
        }
        if (leftCloseDetected == true)
        {
            tempXMin = -ratInnerVariable;
        }
        if (rightCloseDetected == true)
        {
            tempXMax = ratInnerVariable;
        }

///// if a rat is close, 75% chance of attaching to it. 
///


        if (!isAttached)
        {
            rb.velocity = RandomVector(tempXMin, tempXMax, tempYMin, tempYMax); //Change velocity based on a random vector with the mins and maxes
        }
    }

    private Vector2 RandomVector(float xMin, float xMax, float yMin, float yMax) //vector for movement
    {
        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);
        return new Vector2(x * speed, y * speed);
    }

    private void ColliderCheck() //Used to check whether or not there are rats/uninfected/infected in range
    {
        //If rat is too close
        //   Bool     Collider2D   Overlapping   ContactFilter     Array        Amount
        upCloseDetected = upClose.OverlapCollider(ratContactFilter, upCloseDetection) > 0;
        downCloseDetected = downClose.OverlapCollider(ratContactFilter, downCloseDetection) > 0;
        leftCloseDetected = leftClose.OverlapCollider(ratContactFilter, leftCloseDetection) > 0;
        rightCloseDetected = rightClose.OverlapCollider(ratContactFilter, rightCloseDetection) > 0;

        //If rat is within outer range
        upRatDetected = upRat.OverlapCollider(ratContactFilter, upRatDetection) > 1;
        downRatDetected = downRat.OverlapCollider(ratContactFilter, downRatnDetection) > 1;
        leftRatDetected = leftRat.OverlapCollider(ratContactFilter, leftRatDetection) > 1;
        rightRatDetected = rightRat.OverlapCollider(ratContactFilter, rightRatDetection) > 1;

    }

    public void ChangeRatsSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
