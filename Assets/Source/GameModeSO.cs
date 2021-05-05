using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData_", menuName = "ScriptableObjects/New Game Mode Rules", order = 1)]
public class GameModeSO : ScriptableObject
{
    [Tooltip("Measured in SECONDS")]
    [SerializeField] public float GameModeTimeInSeconds;

    [SerializeField] public PointsData DropZone;
    [SerializeField] public ScoreTriggerGameRules RingShots;
    [SerializeField] public ScoreTriggerGameRules TowerZoneTop, TowerZoneMiddle, TowerZoneLower;
    [SerializeField] public ScoreTriggerGameRules StartLinesDropZone;
    [SerializeField] public ScoreTriggerGameRules EndGameDropZones;
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
