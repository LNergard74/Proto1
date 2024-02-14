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
    [SerializeField] GameObject GameManager;

    /// <summary>
    /// when the trigger gets hit it kills the preist
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chandelier")
        {
            GameManager.GetComponent<GameManager>().remove(NPC);
            Destroy(NPC);
        }
    }
}
