using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaundryChute : MonoBehaviour
{
    public GhostMovement Player;

    public GameObject otherChute;
    public GameObject book;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<GhostMovement>();
    }

    public void sendBook()
    {
        Instantiate(book, otherChute.transform.position, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
