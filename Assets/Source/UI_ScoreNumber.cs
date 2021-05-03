using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ScoreNumber : MonoBehaviour
{
    public Team team;
    private TextMeshProUGUI uiScore;

    // Start is called before the first frame update
    void Start()
    {

        uiScore = GetComponentInChildren<TextMeshProUGUI>();
        uiScore.text = "0";
        FindObjectOfType<ScoringSystem>().OnNewScoreUpdate += UpdateScore;
    }

    private void UpdateScore(int value, Team team)
    {
        if(team == this.team)
            uiScore.text = value.ToString();
    }
}
