//This script is just to test the possession and can be deleted after the first playtest

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugEnemyScript : MonoBehaviour
{
    /*This script has basic movement controls for whene the ghost possesses it.
     *To try and combat the issue of both enemies moving at the same time I had
     *tried making the possession distance based but then it just wouldn't move
     *all. 
     *
     *The distance var is coded out in the intial if statement for now because 
     *it prevented the ghost from moving.
    */


    private Vector2 movement;
    private Rigidbody2D rb2d;
    [SerializeField] private float moveSpeed;
    private Movement movementActions;

    private GameObject player;

    private float disToPlayer;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movementActions = new Movement();
        rb2d.simulated = false;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        GhostMovement gm = GameObject.FindObjectOfType<GhostMovement>();

        if (gm.canPossess) //&& disToPlayer == 2f)
        {
            rb2d.simulated = true;
        }
        else
        {
            rb2d.simulated = false;
        }
    }
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        //rb2d.AddForce(moveSpeed * movement);
    }

    private void Update()
    {
        player = GameObject.Find("Ghost");
        disToPlayer = Vector2.Distance(transform.position, player.transform.position);
    }


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
