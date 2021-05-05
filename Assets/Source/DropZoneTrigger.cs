using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneTrigger : ScoreTrigger
{    
    public override void OnTriggerEnter(Collider other)
    {
        ScoringEventCheck(other, scoreValue, team);
    }

    public void OnTriggerExit(Collider other)
    {
        ScoringEventCheck(other, -scoreValue, team);
    }

    public void ScoringEventCheck(Collider other, int value, Team team)
    {
        if(isActivated)
        {
            if (other.CompareTag(objectTag))
            {
                if (other.gameObject.GetComponentInParent<ScoringObjectDataContainer>())
                {
                    if (team == Team.none)
                    {
                        Debug.Log(string.Format("<color=red>Score triggered by {0}</color>", objectTag));
                        TriggerScoreEvent(value, other.GetComponentInParent<ScoringObjectDataContainer>().team);
                    }

                    if (other.gameObject.GetComponentInParent<ScoringObjectDataContainer>().team == this.team)
                    {
                        Debug.Log(string.Format("<color=red>Score triggered by {0}</color>", objectTag));
                        TriggerScoreEvent(value, team);
                    }
                }
            }
        }
    }
}
