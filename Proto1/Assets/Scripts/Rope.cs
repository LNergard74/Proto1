using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject Chandelier;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DroppedBook")
        {
            Chandelier.GetComponent<chandelier>().canFall = true;
        }
    }
}
