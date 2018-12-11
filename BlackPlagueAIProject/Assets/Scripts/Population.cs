using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    
    public static int numRats=2;
    public static int numUninfected=4;
    public static int numInfected;
    public static int numFleas;

    [SerializeField]
    private float timeToRepopulate;
    [SerializeField]
    private float ratRepopulationPercentage; //Percentage of current population to add as new objects
    [SerializeField]
    private float uninfectedRepopulationPercentage; //Percentage of current population to add as new objects
    [SerializeField]
    private float fleasRepopulationPercentage; //Percentage of current population to add as new objects

    [SerializeField]
    private Collider2D objectFinder;

    [SerializeField]
    private ContactFilter2D ratFilter;
    [SerializeField]
    private ContactFilter2D uninfectedFilter;
    [SerializeField]
    private ContactFilter2D fleasFilter;

    //prefabs to instantiate
    [SerializeField]
    private GameObject rat;
    [SerializeField]
    private GameObject uninfected;
    [SerializeField]
    private GameObject fleas;

    private Collider2D[] uninfectedDetection = new Collider2D[50];
    private Collider2D[] fleasDetection = new Collider2D[50];
    private Collider2D[] ratDetection = new Collider2D[50];

    private int ratsToAdd;
    private int uninfectedToAdd;
    private int fleasToAdd;

    private float timeToAdd;
    private Human human;

    private void Start()
    {
        timeToAdd = timeToRepopulate;
    }

    private void FixedUpdate()
    {
       // CheckNumberOfEntities();

        if (Time.time>=timeToRepopulate)
        {
            
            if(numRats>=2)
            {
               ratsToAdd = Mathf.RoundToInt(numRats * ratRepopulationPercentage);
                for (int x = 1; x <= ratsToAdd; x++)
                {
                    Instantiate(rat);
                    IncrementRats();
                }
            }
            if(numUninfected>=2)
            {
               uninfectedToAdd = Mathf.RoundToInt(numUninfected * uninfectedRepopulationPercentage);
                for (int y = 1; y <= uninfectedToAdd; y++)
                {
                    Instantiate(uninfected);
                    IncrementUninfected();

                }
            }
            if(numFleas>=2)
            {
               fleasToAdd = Mathf.RoundToInt(numFleas * fleasRepopulationPercentage);
            /*  Add Flea prefab to serialize field in population object
              for(int z=1;z<=fleasToAdd;z++)
              {
                  Instantiate(fleas);
                  IncrementFleas();
              }
            */
            }
            
            timeToRepopulate += timeToAdd;
        }
    }

    /*
    private void CheckNumberOfEntities()
    {
        numRats = objectFinder.OverlapCollider(ratFilter, ratDetection);
        numUninfected = objectFinder.OverlapCollider(uninfectedFilter, uninfectedDetection);
        numFleas = objectFinder.OverlapCollider(fleasFilter, fleasDetection);
    }
    */

    public void IncrementRats()
    {
        numRats++;
    }
    public void IncrementUninfected()
    {
        numUninfected++;
    }
    public void IncrementInfected()
    {
        numInfected++;
    }
    public void IncrementFleas()
    {
        numFleas++;
    }

    public void DecrementRats()
    {
        numRats--;
    }
    public void DecrementUninfected()
    {
        numUninfected--;
    }
    public void DecrementInfected()
    {
        numInfected--;
    }
    public void DecrementFleas()
    {
        numFleas--;
    }
}
