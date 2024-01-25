/*****************************************************************************
// File Name :         GhostMovement.cs
// Author :            Lorien Nergard
// Creation Date :     Jan 23rd, 2024
//
// Brief Description : Controls the movement of the ghost and controls the
possession behavior. 
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb2d;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float possessionRange;

    private Movement movementActions;

    public bool isPossessed;
    public bool canPossess;
    private Vector3 targetLocation;

    private GameObject cEnemy;

    /// <summary>
    /// Finds the rigidbody, assigns the action map to movementActions, and sets the isPossessed bool
    /// </summary>
    private void Awake()
    {
        movementActions = new Movement();
        rb2d = GetComponent<Rigidbody2D>();
        isPossessed = false;
    }

    /// <summary>
    /// When you press 'E' this is what checks to see if you're possessing something or not. 
    /// This also controls the appearance/behavior of the ghost when possessing an enemy.
    /// </summary>
    /// <param name="context"></param>
    private void Interact(InputAction.CallbackContext context)
    {
        if (!isPossessed && canPossess)
        {
            isPossessed = true;
            //rb2d.simulated = false;     Freezes the rigidbody so it won't move
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            cEnemy.GetComponent<PersonMovement>().possesed();
        }
        else
        {
            isPossessed = false;
            rb2d.simulated = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            cEnemy.GetComponent<PersonMovement>().unpossesed();
        }
    }

    /// <summary>
    /// Gets the value from pressed keys and assigns that value to the var movement
    /// </summary>
    /// <param name="value"></param>
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    /// <summary>
    /// This adds force to the rigidbody to move it
    /// It's using AddForce to give the ghost that 'floaty' effect
    /// </summary>
    private void FixedUpdate()
    {
        if (isPossessed)
        {
            movement.y = 0;
            rb2d.AddForce(moveSpeed * movement);
            cEnemy.transform.position = transform.position;
        }
        else
        {
            rb2d.AddForce(moveSpeed * movement);
        }
    }

    /// <summary>
    /// Updates the ClosestEnemy function
    /// </summary>
    private void Update()
    {
        cEnemy = ClosestEnemy();

        //Updating the targetLocation for the player to follow when possessing enemies
        if (isPossessed)
        {
            transform.position = targetLocation;
        }
    }

    /// <summary>
    /// Finds the distance to the closest enemy
    /// Also checks if the ghost is close enough to possess the enemy
    /// </summary>
    /// <returns></returns>
    private GameObject ClosestEnemy()
    {
        GameObject[] enemy;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        
        //Checking through each enemy to see which one is closest
        foreach (GameObject go in enemy)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                
                //Allows the player to possess enemies once close enough to enemy
                if(curDistance < possessionRange)
                {
                    canPossess = true;
                    targetLocation = go.transform.position;
                }
                else
                {
                    canPossess = false;
                }
            }
        }
        
        return closest;
    }

    /// <summary>
    /// Enables and disables controls
    /// </summary>
    private void OnEnable()
    {
        movementActions.Enable();
        movementActions.Controls.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        movementActions.Disable();
        movementActions.Controls.Interact.performed -= Interact;
    }
}
