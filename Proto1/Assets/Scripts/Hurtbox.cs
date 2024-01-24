using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] GameObject NPC;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Book")
        {
            Destroy(collision.gameObject);
            Destroy(NPC);
        }
    }
}
