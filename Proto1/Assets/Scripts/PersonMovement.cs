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
    private SpriteRenderer sr;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private float slowTime;
    [SerializeField] private float stunnedTime;

    [SerializeField] private Sprite UnPossessedSprite;
    [SerializeField] private Sprite PossessedSprite;

    private GameObject target;

    bool walking = true;
    bool Possesed = false;
    bool slowed = false;

    Animator Animator;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        target = Point1;
        Animator = gameObject.GetComponent<Animator>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chandelier")
        {
            if (!slowed)
            {
                moveSpeed = moveSpeed / 2;
                Invoke("resetSpeed", slowTime);
                slowed = true;
            }
        }
    }

    void resetSpeed()
    {
        moveSpeed = baseMoveSpeed;
        slowed = false;
    }

    private void resetCooldown()
    {
        if (!Possesed)
        {
            Animator.SetTrigger("Unstun");
            walking = true;
        }
    }

    public void possesed()
    {
        //rb.velocity = Vector3.zero;
        walking = false;
        Possesed = true;
        sr.sprite = PossessedSprite;
    }

    public void unpossesed()
    {
        Animator.SetTrigger("Stun");
        Possesed = false;
        Invoke("resetCooldown", stunnedTime);
        sr.sprite = UnPossessedSprite;
    }
}
