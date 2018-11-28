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
    float movementSpeed;
    [SerializeField]
    float lifeSpan;
    [SerializeField]
    float spawnRate;

    public void Move()
    {
        //Possible Movement Methods:
        // Randomize 1-10, Each direction (including diagonals and no movement) have 1/10 chance
        // Add velocity according to direction
        // If rat in radius, decrease chance of moving in direction
        // (How to determine the direction? Lots of if Statements?)
        //
        // Random (-1,2) for X (-1 to 0 is left) (0 to 1 is stop) (1 to 2 is right)
        // Random (-1,2) for y (-1 to 0 is down) (0 to 1 is stop) (1 to 2 is up)
        // Add velocity based on directions
        // If rat in radius, determine position, and get rid of x/y movement in direction of rat, or decrease chance of it. 
        // Random (-0.5(?), 1) for X
        // Random (-1, 1) for Y
        // Makes less likely to move left towards rat. 

        //Used to determine chances of human moving in certain directions
        float xMin = -1; 
        float xMax = 2;
        float yMin = -1;
        float yMax = 2;

        float xDirection = Random.Range(xMin, xMax);
        float yDirection = Random.Range(yMin, yMax);

    }

}
