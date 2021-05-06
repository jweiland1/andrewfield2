using UnityEngine;
using TMPro;

public class RulesManager : MonoBehaviour
{
    [Tooltip("In Sequential Order")]
    [SerializeField] private GameModeSO [] GameModeTimeIntervals;

    [Header("Score Triggers")]
    [Tooltip("Arbitary text message")]
    [SerializeField] private DropZoneTrigger[] DropZones;
    [SerializeField] private ScoreTrigger[] EndGameDropZones;
    [SerializeField] private ScoreTrigger[] RingShots;
    [SerializeField] private ScoreTrigger[] TowerZones;
    [SerializeField] private ScoreTrigger[] StartLines;

    [SerializeField] private TextMeshProUGUI [] textString;

    [SerializeField] private ObjectStack[] RingStacks;
    [SerializeField] private GameObject RingPrefab;

    private InventoryContainer ringOP;

    private int currentRuleIndex = 0;

    //randomize the drop zone and set ring stack

    [SerializeField] private Timer timer;
    private void Start()
    {
        if(!timer)
        {
            timer = GetComponent<Timer>();
        }

        CreateObjectPool();
        SetupNewRules();
        PickRandomMode();
    }

    private void CreateObjectPool()
    {
        ringOP = GetComponent<InventoryContainer>();        

        for(int i = 0; i < ringOP.inventory.maxSize; i++)
        {
            ringOP.inventory.AddToInventory(Instantiate(RingPrefab));
        }
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

    private void PickRandomMode()
    {
        int randomMode = Random.Range(1, 4);

        for(int i = 0; i < DropZones.Length; i++)
        {
            if(randomMode == 1 && DropZones[i].zoneID.Equals('A'))
            { 
                DropZones[i].isActivated = true;
                textString[0].text = "Drop Zone: A";                
                continue;                
            }
            else if (randomMode == 2 && DropZones[i].zoneID.Equals('B'))
            {
                DropZones[i].isActivated = true;
                textString[0].text = "Drop Zone: B";                
                continue;
            } 
            else if (randomMode == 3 && DropZones[i].zoneID.Equals('C'))
            {
                DropZones[i].isActivated = true;
                textString[0].text = "Drop Zone: C";                
                continue;
            }

            DropZones[i].isActivated = false;
        }

        if (randomMode == 1)
        {
            for (int a = 0; a < RingStacks.Length; a++)
            {
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
            }
        }
        else if(randomMode == 2)
        {
            for (int a = 0; a < RingStacks.Length; a++)
            {
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
            }
        }
        else if (randomMode == 3)
        {
            for (int a = 0; a < RingStacks.Length; a++)
            {
                RingStacks[a].AddToStack(ringOP.inventory.PopFromInventory());
            }
        }

    }

    
    private void SetupNewRules()
    {
        if (currentRuleIndex >= GameModeTimeIntervals.Length)
            return;

        // setup rule timer
        timer.SetNewTimedEvent(GameModeTimeIntervals[currentRuleIndex].GameModeTimeInSeconds, isRepeating: false);
        timer.OnTimedEvent += TimedEventHandler;

        // update drop zones
        for(int i = 0; i < DropZones.Length; i++)
        {
            DropZones[i].scoreValue = GameModeTimeIntervals[currentRuleIndex].DropZone.pointsValue;
        }

        UpdateScoreTriggerGameRules(EndGameDropZones, GameModeTimeIntervals[currentRuleIndex].EndGameDropZones);
        UpdateScoreTriggerGameRules(RingShots, GameModeTimeIntervals[currentRuleIndex].RingShots);        
        UpdateScoreTriggerGameRules(StartLines, GameModeTimeIntervals[currentRuleIndex].StartLinesDropZone);

        TowerZones[0].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneTop.pointsValue;
        TowerZones[1].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneMiddle.pointsValue;
        TowerZones[2].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneLower.pointsValue;
        TowerZones[3].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneTop.pointsValue;
        TowerZones[4].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneMiddle.pointsValue;
        TowerZones[5].scoreValue = GameModeTimeIntervals[currentRuleIndex].TowerZoneLower.pointsValue;

        textString[1].text = "Game Mode: " + GameModeTimeIntervals[currentRuleIndex].name;
        textString[2].text = "Mode Timer: " + GameModeTimeIntervals[currentRuleIndex].GameModeTimeInSeconds.ToString();
        textString[3].text = "Drop Zone Points: " + GameModeTimeIntervals[currentRuleIndex].DropZone.pointsValue.ToString();
        textString[4].text = "Ring Shot Points: " + GameModeTimeIntervals[currentRuleIndex].RingShots.pointsValue.ToString();
        textString[5].text = "Tower Top: " + GameModeTimeIntervals[currentRuleIndex].TowerZoneTop.pointsValue.ToString();
        textString[6].text = "Tower Mid: " + GameModeTimeIntervals[currentRuleIndex].TowerZoneMiddle.pointsValue.ToString();
        textString[7].text = "Tower Low: " + GameModeTimeIntervals[currentRuleIndex].TowerZoneLower.pointsValue.ToString();
        textString[8].text = "Start Line Drop Zone: " + GameModeTimeIntervals[currentRuleIndex].StartLinesDropZone.pointsValue.ToString();
        textString[9].text = "End Game Drop Zone: " + GameModeTimeIntervals[currentRuleIndex].EndGameDropZones.pointsValue.ToString();
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
