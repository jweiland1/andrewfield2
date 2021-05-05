using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public bool isActivated = true;
    public int scoreValue = 1;
    public Team team;

    public delegate void ScoreEvent(int value, Team team);
    public event ScoreEvent OnScoreTriggered;
    [SerializeField] protected string objectTag;

    private void Start()
    {
        FindObjectOfType<ScoringSystem>().AddMeToScoreSystem(this);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(isActivated)
        {
            if(other.CompareTag(objectTag))
            {
                //Debug.Log("Score triggered by " + objectTag);
                TriggerScoreEvent(scoreValue, team);
            }
        }
    }

    protected void TriggerScoreEvent(int value, Team team)
    {
        OnScoreTriggered?.Invoke(value, team);
    }
}