using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public int scoreValue = 1;
    public Team team;

    public delegate void ScoreEvent(int value, Team team);
    public event ScoreEvent OnScoreTriggered;

    private void Start()
    {
        FindObjectOfType<ScoringSystem>().AddMeToScoreSystem(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ring"))
        {
            OnScoreTriggered(scoreValue, team);
        }
    }
}