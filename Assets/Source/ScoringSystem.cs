using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Team  { red, blue, none }

public class ScoringSystem : MonoBehaviour
{
    public delegate void ScoreEvent(int value, Team team);
    public event ScoreEvent OnNewScoreUpdate;

    public int BlueTeam = 0;
    public int RedTeam = 0;

    public void AddMeToScoreSystem(ScoreTrigger newSubscriber)
    {
        newSubscriber.OnScoreTriggered += AddToScore;
        Debug.Log("add new sub: " + newSubscriber.name);
    }

    public void AddToScore(int value, Team team)
    {
        switch (team)
        {
            case Team.red: 
                RedTeam += value;
                OnNewScoreUpdate?.Invoke(RedTeam, team);
                break;
            case Team.blue: 
                BlueTeam += value;
                OnNewScoreUpdate?.Invoke(BlueTeam, team);
                break;
        }

        Debug.Log("Team " + team.ToString());        
    }

    private void OnDestroy()
    {
        ScoreTrigger [] subs = FindObjectsOfType<ScoreTrigger>();
        foreach(ScoreTrigger s in subs)
        {
            s.OnScoreTriggered -= AddToScore;
        }
    }
}
