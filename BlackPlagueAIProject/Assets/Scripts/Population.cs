using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    public int numRats;
    public int numUninfected;
    public int numFleas;

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

    private void Start()
    {
        timeToAdd = timeToRepopulate;
    }

    private void FixedUpdate()
    {
        if(Time.time>=timeToRepopulate)
        {
            checkNumberOfEntities();
            if(numRats>=2)
            {
               ratsToAdd = Mathf.RoundToInt(numRats * (1/ratRepopulationPercentage));
                for (int x = 0; x <= ratsToAdd; x++)
                {
                    Instantiate(rat);
                }
            }
            if(numUninfected>=2)
            {
               uninfectedToAdd = Mathf.RoundToInt(numUninfected * (1/uninfectedRepopulationPercentage));
                for (int y = 0; y <= uninfectedToAdd; y++)
                {
                    Instantiate(uninfected);
                }
            }
            if(numFleas>=2)
            {
               fleasToAdd = Mathf.RoundToInt(numFleas * (1/fleasRepopulationPercentage));
            /*  Add Flea prefab to serialize field in population object
              for(int z=0;z<=fleasToAdd;z++)
              {
                  Instantiate(fleas);
              }
            */
            }

            Debug.Log("Current number of uninfected: " + numUninfected);
            timeToRepopulate += timeToAdd;
        }
    }


    private void checkNumberOfEntities()
    {
        numRats = objectFinder.OverlapCollider(ratFilter, ratDetection);
        numUninfected = objectFinder.OverlapCollider(uninfectedFilter, uninfectedDetection);
        numFleas = objectFinder.OverlapCollider(fleasFilter, fleasDetection);
    }


}
