using UnityEngine;
using TMPro;

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

    [SerializeField] private TextMeshProUGUI [] textString;

    private int currentRuleIndex = 0;

    //randomize the drop zone and set ring stack

    [SerializeField] private Timer timer;
    private void Start()
    {
        if(!timer)
        {
            timer = FindObjectOfType<Timer>();
        }

        SetupNewRules();
    }

    /// <summary>
    ///  Called when timer is up
    /// </summary>
    private void TimedEventHandler()
    {
        timer.OnTimedEvent -= TimedEventHandler;
        currentRuleIndex++;
        SetupNewRules();
    }

    
    private void SetupNewRules()
    {
        if (currentRuleIndex >= GameModeTimeIntervals.Length)
            return;

        // setup rule timer
        timer.SetNewTimedEvent(GameModeTimeIntervals[currentRuleIndex].GameModeTimeInSeconds);
        timer.OnTimedEvent += TimedEventHandler;

        // update drop zones
        for(int i = 0; i < DropZones.Length; i++)
        {
            DropZones[currentRuleIndex].scoreValue = GameModeTimeIntervals[currentRuleIndex].DropZone.pointsValue;
        }

        UpdateScoreTriggerGameRules(EndGameDropZones, GameModeTimeIntervals[currentRuleIndex].EndGameDropZones);
        UpdateScoreTriggerGameRules(RingShots, GameModeTimeIntervals[currentRuleIndex].RingShots);        
        UpdateScoreTriggerGameRules(StartLines, GameModeTimeIntervals[currentRuleIndex].StartLinesDropZone);

        TowerZones[0].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneTop.pointsValue;
        TowerZones[1].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneMiddle.pointsValue;
        TowerZones[2].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneLower.pointsValue;

        textString[0].text = "Game Mode: " + GameModeTimeIntervals[currentRuleIndex].name;
        textString[1].text = "Mode Timer: " + GameModeTimeIntervals[currentRuleIndex].GameModeTimeInSeconds.ToString();
        textString[2].text = "Drop Zone Points: " + GameModeTimeIntervals[currentRuleIndex].DropZone.pointsValue.ToString();
        textString[3].text = "Ring Shot Points: " + GameModeTimeIntervals[currentRuleIndex].RingShots.pointsValue.ToString();
        textString[4].text = "Tower Top: " + GameModeTimeIntervals[currentRuleIndex].TowerZoneTop.pointsValue.ToString();
        textString[5].text = "Tower Mid: " + GameModeTimeIntervals[currentRuleIndex].TowerZoneMiddle.pointsValue.ToString();
        textString[6].text = "Tower Low: " + GameModeTimeIntervals[currentRuleIndex].TowerZoneLower.pointsValue.ToString();
        textString[7].text = "Start Line Drop Zone: " + GameModeTimeIntervals[currentRuleIndex].StartLinesDropZone.pointsValue.ToString();
        textString[8].text = "End Game Drop Zone: " + GameModeTimeIntervals[currentRuleIndex].EndGameDropZones.pointsValue.ToString();
    }

    private void UpdateScoreTriggerGameRules(ScoreTrigger [] to, ScoreTriggerGameRules from)
    {
        for (int i = 0; i < to.Length; i++)
        {
            to[i].scoreValue = from.pointsValue;
        }
    }

    private void OnDisable()
    {
        timer.OnTimedEvent -= TimedEventHandler;
    }

}
