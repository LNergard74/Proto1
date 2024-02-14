using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chandelier : MonoBehaviour
{
    public bool canFall;

    public Rigidbody2D myRB;

    public bool resetting;
    public GameObject defaultPossition;
    public float resetSpeed;
    public float resetTime;

    public GameObject hitbox;

    private void Start()
    {
        myRB = gameObject.GetComponent<Rigidbody2D>();
    }

    //To check if the chandelier should go back to the spot where it fell
    private void Update()
    {
        if (resetting)
        {
            hitbox.GetComponent<PolygonCollider2D>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, defaultPossition.transform.position, resetSpeed * Time.deltaTime);
            if(transform.position == defaultPossition.transform.position)
            {
                myRB.velocity = Vector3.zero;
                resetting= false;
            }
        }
    }

    //called to make the chandelier fall to start damaging the preists
    public void drop()
    {
        resetting= false;
        myRB.gravityScale = 1;
        Invoke("goback", resetTime);
        hitbox.GetComponent<PolygonCollider2D>().enabled = true;
    }

    //starts to reset the chandeler back to its original possition
    public void goback()
    {
        myRB.gravityScale = 0;
        resetting = true;
    }
}
