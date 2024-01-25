/*****************************************************************************
// File Name :         PersonMovement.cs
// Author :            Brenden Burtz
// Creation Date :     Jan 23rd, 2024
//
// Brief Description : Controlls how the exorsists act when not possessed. 
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PersonMovement : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Player;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private float stunnedTime;

    private GameObject target;

    bool walking = true;
    bool Possesed = false;


    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        target = Point1;
    }

    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        if (walking)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

            if (transform.position.x == Point2.transform.position.x)
            {
                target = Point1;
                walking = false;
                //rb.velocity = Vector3.zero;
                Invoke("resetCooldown", waitTime);
            }
            else if(transform.position.x == Point1.transform.position.x)
            {
                target = Point2;
                walking = false;
                //rb.velocity = Vector3.zero;
                Invoke("resetCooldown", waitTime);
            }
        }
    }

    private void resetCooldown()
    {
        if (!Possesed)
        {
            walking = true;
        }
    }

    public void possesed()
    {
        //rb.velocity = Vector3.zero;
        walking = false;
        Possesed = true;
    }

    public void unpossesed()
    {
        Possesed = false;
        Invoke("resetCooldown", stunnedTime);
    }
}
