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
    private  float xMin = -1;
    private float xMax = 1;
    private float yMin = -1;
    private float yMax = 1;


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
        rb.velocity=RandomVector(xMin, xMax, yMin, yMax);

    }

    private Vector2 RandomVector(float xMin, float xMax, float yMin, float yMax)
    {
        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);
        return new Vector2(x*speed, y*speed);
    }

}
