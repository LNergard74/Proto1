using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDestroy : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnDestroy()
    {
        player.GetComponent<GhostMovement>().Bonk();
    }
}
