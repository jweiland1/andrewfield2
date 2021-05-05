using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData_", menuName = "ScriptableObjects/New Game Mode Rules", order = 1)]
public class GameModeSO : ScriptableObject
{
    [Tooltip("Measured in SECONDS")]
    [SerializeField] private float GameModeTimeInSeconds;


    [SerializeField] private PointsData DropZone;
    [SerializeField] private ScoreTriggerGameRules RingShots;
    [SerializeField] private ScoreTriggerGameRules TowerZoneTop, TowerZoneMiddle, TowerZoneLower;
    [SerializeField] private ScoreTriggerGameRules StartLinesDropZone;
    [SerializeField] private ScoreTriggerGameRules EndGameDropZones;
}

[System.Serializable]
public struct ScoreTriggerGameRules
{
    public bool isActivated;
    public int pointsValue;
    public bool isTeamSensitive;
}

[System.Serializable]
public struct PointsData
{
    public int pointsValue;
}
