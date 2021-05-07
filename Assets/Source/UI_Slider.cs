using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour
{
    private Slider uiScore;

    // Start is called before the first frame update
    void Start()
    {
        uiScore = GetComponent<Slider>();
        FindObjectOfType<RingLauncher>().OnPowerChanged += UpdateSlider;
        uiScore.maxValue = FindObjectOfType<RingLauncher>().flywheelMaxPower;
        uiScore.value = FindObjectOfType<RingLauncher>().flywheelPower;
    }

    // Update is called once per frame
    void UpdateSlider(float value)
    {
        uiScore.value += value;
    }
}
