using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropZoneTrigger : ScoreTrigger
{
    public char zoneID;
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
                        TriggerScoreEvent(value, other.GetComponentInParent<ScoringObjectDataContainer>().team);
                    }

                    if (other.gameObject.GetComponentInParent<ScoringObjectDataContainer>().team == this.team)
                    {                        
                        TriggerScoreEvent(value, team);
                    }
                }
            }
        }
    }
}
