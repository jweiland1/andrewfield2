using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Team  { red, blue }

public class ScoringSystem : MonoBehaviour
{
    public int BlueTeam = 0;
    public int RedTeam = 0;
        
    public void AddMeToScoreSystem(ScoreTrigger newSubscriber)
    {
        newSubscriber.OnScoreTriggered += AddToScore;
        Debug.Log("add new sub: " + newSubscriber.name);
    }

    public void AddToScore(int value, Team team)
    {
        switch(team)
        {
            case Team.red: RedTeam += value; break;
            case Team.blue: BlueTeam += value; break;
        }

        Debug.Log("Team " + team.ToString());
    }
}
