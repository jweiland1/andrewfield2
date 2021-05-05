using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool isTimedEvent = false;
    public float timedEvent;// in seconds
    
    public float intervalInSeconds = 1; // in seconds
    public float timerLength = 120;
    public bool timerIsRunning = false;

    public delegate void UpdateTime(string newTime);
    public event UpdateTime OnTimerUpdate;

    public delegate void TimedEvent();
    public event TimedEvent OnTimedEvent;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    public void SetNewTimedEvent(float timeInSeconds)
    {
        isTimedEvent = true;
        timedEvent = timeInSeconds;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (intervalInSeconds > 0)
            {
                intervalInSeconds -= Time.deltaTime;
                timerLength -= Time.deltaTime;
            }
            else
            {
                intervalInSeconds = 1;
                OnTimerUpdate?.Invoke(DisplayTime(timerLength));
            }

            if(isTimedEvent)
            {
                if(timedEvent > 0)
                {
                    timedEvent -= Time.deltaTime;
                }
                else
                {
                    timedEvent = 0;
                    isTimedEvent = false;
                    OnTimedEvent?.Invoke();
                }
            }
        }
    }

    public string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
