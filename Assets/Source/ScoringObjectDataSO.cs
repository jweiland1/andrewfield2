using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Scoring Object Data", order = 1)]
public class ScoringObjectDataSO : ScriptableObject
{
    public string type;    
    public float Mass { get; set; }
}
