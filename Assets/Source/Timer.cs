using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{    
    public float eventTime;// in seconds
    private float remainingTime;
    public bool timerIsRunning = false;
    public bool isRepeating = false;

    public delegate void TimedEvent();
    public event TimedEvent OnTimedEvent;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    public void SetNewTimedEvent(float timeInSeconds, bool isRepeating)
    {
        timerIsRunning = true;
        eventTime = timeInSeconds;
        remainingTime = eventTime;
        this.isRepeating = isRepeating;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if(remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                if(isRepeating)
                {
                    remainingTime = eventTime;
                }
                else
                {
                    eventTime = 0;
                    timerIsRunning = false;         
                }
                OnTimedEvent?.Invoke();
            }            
        }
    }    
}
