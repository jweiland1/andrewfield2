using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textString;
    Timer timer;
    public float updateInterval = 1; //in seconds
    public float timeLimit = 120; // in seconds

    private void Awake()
    {
        if (!textString)
            textString = GetComponent<TextMeshProUGUI>();

        timer = GetComponent<Timer>();
        timer.SetNewTimedEvent(updateInterval, isRepeating: true);
        DisplayTime(timeLimit);
    }

    void OnEnable()
    {
        timer.OnTimedEvent += UpdateTextString;
    }

    private void OnDisable()
    {
        timer.OnTimedEvent -= UpdateTextString;
    }

    
    public void UpdateTextString()
    {
        timeLimit -= 1;
        if (timeLimit <= 0)
            timeLimit = 0;

        textString.text = DisplayTime(timeLimit);
    }

    public string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}