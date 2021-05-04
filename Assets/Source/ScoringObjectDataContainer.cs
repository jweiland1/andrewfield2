using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringObjectDataContainer : MonoBehaviour
{
    public Team team;
    public ScoringObjectDataSO data;

    private void Awake()
    {
        data.Mass = GetComponent<Rigidbody>().mass;
    }
}
