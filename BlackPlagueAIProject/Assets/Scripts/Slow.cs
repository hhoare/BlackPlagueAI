using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour {

    private int ratLayer = 10;
    private int uninfectedLayer = 11;
    private int infectedLayer = 12;
    private float storedRatSpeed;
    private float storedUninfectedSpeed;
    private float storedInfectedSpeed;

    private Rat rat;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==ratLayer)
        {
            rat=collision.gameObject.GetComponent<Rat>();
            storedRatSpeed = rat.GetRatSpeed();
            rat.ChangeRatsSpeed(storedRatSpeed/4); //Halves the current speed of the rat
        }
        if(collision.gameObject.layer==uninfectedLayer)
        {
            Human human = collision.gameObject.GetComponent<Human>();
            storedUninfectedSpeed = human.GetHumanSpeed();
            human.ChangeHumansSpeed(storedUninfectedSpeed / 4);
        }
        if (collision.gameObject.layer == infectedLayer)
        {
            Human human = collision.gameObject.GetComponent<Human>();
            storedInfectedSpeed = human.GetHumanSpeed();
            human.ChangeHumansSpeed(storedInfectedSpeed / 4);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == ratLayer)
        {
            rat = collision.gameObject.GetComponent<Rat>();
            rat.ChangeRatsSpeed(storedRatSpeed); //Returns speed to the stored value
        }
        if (collision.gameObject.layer == uninfectedLayer)
        {
            Human human = collision.gameObject.GetComponent<Human>();
            human.ChangeHumansSpeed(storedUninfectedSpeed);
        }
    }
    */

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D name = collision.gameObject.GetComponent<Rigidbody2D>();
        name.velocity = name.velocity / 1.15f;
    }
}
