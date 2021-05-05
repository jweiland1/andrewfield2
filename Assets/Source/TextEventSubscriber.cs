using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEventSubscriber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textString;

    private void Start()
    {
        if (!textString)
            textString = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        FindObjectOfType<Timer>().OnTimerUpdate += UpdateTextString;
    }

    private void OnDisable()
    {        
        FindObjectOfType<Timer>().OnTimerUpdate -= UpdateTextString;
    }

    public void UpdateTextString(string newText)
    {
        textString.text = newText;
    }
}
