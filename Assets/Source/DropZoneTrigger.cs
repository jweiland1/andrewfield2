using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneTrigger : ScoreTrigger
{    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            if(other.gameObject.GetComponentInParent<ScoringObjectDataContainer>())
            {
                if(other.gameObject.GetComponentInParent<ScoringObjectDataContainer>().team == this.team)
                {
                    Debug.Log("Score triggered by " + objectTag);
                    TriggerScoreEvent(scoreValue, team);
                }
            }
        }
    }
}
