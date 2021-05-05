using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    [Tooltip("In Sequential Order")]
    [SerializeField] private GameModeSO [] GameModeTimeIntervals;

    [Header("Score Triggers")]
    [Tooltip("Arbitary text message")]
    [SerializeField] private ScoreTrigger [] DropZones;
    [SerializeField] private ScoreTrigger[] EndGameDropZones;
    [SerializeField] private ScoreTrigger[] RingShots;
    [SerializeField] private ScoreTrigger[] TowerZones;
    [SerializeField] private ScoreTrigger[] StartLines;



}
