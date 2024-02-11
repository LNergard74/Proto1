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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GhostMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb2d;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float possessionRange;

    private Movement movementActions;

    public bool isPossessed;
    public bool justPossessed;
    public bool canPossess;
    public bool canPossessChandelier;
    public bool possessedChandelier = false;

    private Vector3 targetLocation;

    private GameObject cEnemy;
    GameObject Chandelier;
    private GameObject closestItem;
    public GameObject cWindow;
    public GameManager gameManager;
    public GameObject gameController;
    public GameObject possessExorsistButton;
    public GameObject possessBookButton;

    //Window Variables


    /// <summary>
    /// Finds the rigidbody, assigns the action map to movementActions, and sets the isPossessed bool
    /// </summary>
    private void Awake()
    {
        movementActions = new Movement();
        rb2d = GetComponent<Rigidbody2D>();
        isPossessed = false;
        Chandelier = GameObject.FindGameObjectWithTag("Chandelier");
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
            justPossessed = true;
            //rb2d.simulated = false;     Freezes the rigidbody so it won't move
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rb2d.velocity = Vector3.zero;
            cEnemy.GetComponent<PersonMovement>().possesed();
            gameObject.layer = 9;
        }
        else if(!possessedChandelier && canPossessChandelier)
        {
            possessedChandelier = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.position = Chandelier.transform.position;
            rb2d.velocity = Vector3.zero;

        }
        //Detects whether the window is broken, if the ghost is currently possessing a priest, and if the player is close enough to the window
        //If all are true, it kills the priest
        else if (isPossessed && Vector3.Distance(cWindow.transform.position, transform.position) < 5f)
        {
            cWindow.GetComponent<WindowBehavior>().breakwindow();
            isPossessed = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameManager.remove(cEnemy);
            Destroy(cEnemy);
            gameObject.layer = 3;
        }
        else if (possessedChandelier)
        {
            possessedChandelier = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Chandelier.GetComponent<chandelier>().drop();
        }
        else if(isPossessed)
        {
            isPossessed = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            cEnemy.GetComponent<PersonMovement>().unpossesed();
            gameObject.layer = 3;
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
        if (isPossessed && !possessedChandelier)
        {
            movement.y = 0;
            rb2d.AddForce(moveSpeed * movement);
            cEnemy.transform.position = transform.position;
        }
        else if(!isPossessed && possessedChandelier)
        {
            transform.position = Chandelier.transform.position;
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
        cWindow = ClosestWindow();

        //Updating the targetLocation for the player to follow when possessing enemies
        if (isPossessed && justPossessed)
        {
            transform.position = targetLocation;
            justPossessed = false;
        }
    }

    /// <summary>
    /// Finds the distance to the closest enemy
    /// Also checks if the ghost is close enough to possess the enemy
    /// </summary>
    /// <returns></returns>
    private GameObject ClosestEnemy()
    {
        if (!isPossessed)
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
                    if (curDistance < possessionRange)
                    {
                        canPossess = true;
                        targetLocation = go.transform.position; 
                        possessExorsistButton.SetActive(true);

                    }
                    else
                    {
                        canPossess = false;
                        possessExorsistButton.SetActive(false);
                    }
                }
            }

            return closest;
        }
        return cEnemy;
    }

   

    private GameObject ClosestWindow()
    {
        if (!isPossessed)
        {
            GameObject[] window;
            window = GameObject.FindGameObjectsWithTag("Window");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            //Checking through each enemy to see which one is closest
            foreach (GameObject go in window)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            return closest;
        }
        return cWindow;
    }
    
    private bool CloseChandelier()
    {
        if (!isPossessed)
        {
            Chandelier = GameObject.FindGameObjectWithTag("Chandelier");
            Vector3 position = transform.position;
            Vector3 diff = Chandelier.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            return (curDistance < possessionRange);
        }
        return false;
    }

    /// <summary>
    /// Enables and disables controls
    /// </summary>
    private void OnEnable()
    {
        movementActions.Enable();
        movementActions.Controls.Interact.performed += Interact;
        movementActions.Controls.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        movementActions.Disable();
        movementActions.Controls.Interact.performed -= Interact;
        movementActions.Controls.Pause.performed -= Pause;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        gameController.GetComponent<GameController>().Pause();
    }
}
