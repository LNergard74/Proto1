using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chandelier : MonoBehaviour
{
    public bool canFall;

    public Rigidbody2D myRB;

    private void Start()
    {
        myRB = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(canFall)
        {
            myRB.gravityScale = 1;
        }
    }
}
