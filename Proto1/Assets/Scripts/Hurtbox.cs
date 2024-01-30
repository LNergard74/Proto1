/*****************************************************************************
// File Name :         Hurtbox.cs
// Author :            Brenden Burtz
// Creation Date :     Jan 23rd, 2024
//
// Brief Description : Destorys the exorsist and book when the hurtbox
is hit.
*****************************************************************************/
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
