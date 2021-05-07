using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ScoringObjectDataContainer : MonoBehaviour
{    
    public Team team;
    public ScoringObjectDataSO data;
    public bool isValidShot = false;

    private void Awake()
    {
        data.Mass = GetComponent<Rigidbody>().mass;
    }
}
