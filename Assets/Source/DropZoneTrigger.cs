using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropZoneTrigger : ScoreTrigger
{
    public char zoneID;
    private Dictionary<int, GameObject> ids;
    public bool isCountingWobblers = false;

    private void Start()
    {
        base.Start();
        ids = new Dictionary<int, GameObject>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        //ScoringEventCheck(other, scoreValue, team);
    }

    public void OnTriggerExit(Collider other)
    {
        //ScoringEventCheck(other, -scoreValue, team);
    }

    private void OnTriggerStay(Collider other)
    {
        if(isCountingWobblers && isActivated)
        {
            if (other.CompareTag(objectTag))
            {
                Debug.Log("wobbler in zone");
                ScoringEventCheck(other.GetComponentInChildren<Collider>(), scoreValue, team);
            }
        }
    }

    private void CountValidWobblers()
    {
        isCountingWobblers = true;
    }

    public void HasLeftMyDropZone(GameObject other)
    {
        ScoringEventCheck(other.GetComponentInChildren<Collider>(), -scoreValue, team);
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
                        if(value > 0)
                        {
                            if (!ids.ContainsKey(other.gameObject.GetComponentInParent<WobblerJobbler>().id))
                            {
                                ids.Add(other.gameObject.GetComponentInParent<WobblerJobbler>().id, other.gameObject);
                                TriggerScoreEvent(value, team);
                            }
                        }
                        else
                        {
                            if (ids.ContainsKey(other.gameObject.GetComponentInParent<WobblerJobbler>().id))
                            {
                                ids.Remove(other.gameObject.GetComponentInParent<WobblerJobbler>().id);
                                TriggerScoreEvent(value, team);
                            }
                        }
                    }
                }
            }
        }
    }
}
