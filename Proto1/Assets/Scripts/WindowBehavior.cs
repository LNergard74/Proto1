using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehavior : MonoBehaviour
{
    public bool windowBroken;
    public Sprite brokenwindow;
    private GhostMovement gmScript;

    // Start is called before the first frame update
    void Start()
    {
        gmScript= GameObject.FindGameObjectWithTag("Player").GetComponent<GhostMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Book") && !windowBroken)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = brokenwindow;
            windowBroken = true;
            Destroy(gmScript.cBook);
        }
    }
}
