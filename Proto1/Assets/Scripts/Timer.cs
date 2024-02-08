using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float maxTime = 120;
    public float timeRemaining;
    public bool timerIsRunning = false;
    [SerializeField] TMP_Text timeText;

    private void Start()
    {
        // Starts the timer y
        TimerStart();
    }
    public void TimerStart()
    {
        timerIsRunning = true;
        timeRemaining = maxTime;
    }

    void Update()
    {
        // this "if" statement runs the timer. If the time is greater then 0 it will keep subtracting.
        // when the timer hits 0 it will print "Time has run out" in the debuglog
        if (timerIsRunning)
        {
            if (timeRemaining > 1)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 1;
                timerIsRunning = false;
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
