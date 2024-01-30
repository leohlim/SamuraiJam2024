using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public float timeToDisplay = 0;
    public bool timerIsRunning = false;

    // References to timer text in game and on victory screen
    public TMP_Text timerText;
    public TMP_Text victoryScreenTimerText;
    


    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerIsRunning)
        {
            timeToDisplay += Time.deltaTime;
        }
        
        DisplayTime();
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 1000;
        Debug.Log(string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds));
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        victoryScreenTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void stopTimer()
    {
        timerIsRunning = false;
    }

    public void restartTimer()
    {
        timeToDisplay = 0;
        timerIsRunning = true;
    }
}
