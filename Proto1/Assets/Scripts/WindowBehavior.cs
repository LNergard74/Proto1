using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void breakwindow()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = brokenwindow;
        windowBroken = true;
    }
}
