using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PersonMovement : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTime;

    private GameObject target;

    bool walking = true;

    private void Start()
    {
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
                Invoke("resetCooldown", waitTime);
            }
            else if(transform.position.x == Point1.transform.position.x)
            {
                target = Point2;
                walking = false;
                Invoke("resetCooldown", waitTime);
            }
        }
    }

    private void resetCooldown()
    {
        walking = true;
    }
}
