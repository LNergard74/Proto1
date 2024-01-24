using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PersonMovement : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTime;

    private GameObject target;

    private bool move1 = false;

    private void Start()
    {
        StartCoroutine(Walk());
    }

    private IEnumerator Walk()
    {
        while (true)
        {
            Debug.Log("In walk");
            if (move1)
            {
                target = Point1;
            }
            else
            {
                target = Point2;
            }

            while (transform.position.x != target.transform.position.x)
            {
                Debug.Log("In While Loop");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }

            move1 = !move1;

            yield return new WaitForSeconds(waitTime);
            yield return null;
        }

    }
}
