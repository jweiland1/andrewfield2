using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RulesManager : MonoBehaviour
{
    public Image img_blueteam, img_redteam;
    public GameObject restart_btn;

    [Tooltip("In Sequential Order")]
    [SerializeField] private GameModeSO [] GameModeTimeIntervals;
    [SerializeField] private ScoringSystem scoringSystem;

    [Header("Score Triggers")]
    [Tooltip("Arbitary text message")]
    [SerializeField] private DropZoneTrigger[] DropZones;
    [SerializeField] private DropZoneTrigger[] EndGameDropZones;
    [SerializeField] private RingShotController[] RingShots;
    [SerializeField] private ScoreTrigger[] TowerZones;
    [SerializeField] private DropZoneTrigger[] StartLines;

    [SerializeField] private TextMeshProUGUI [] textString;

    [SerializeField] private ObjectStack[] RingStacks;
    [SerializeField] private GameObject RingPrefab;
    [SerializeField] private InventoryContainer[] OutsideRingStacks;

    private InventoryContainer ringOP;

    private int currentRuleIndex = 0;

    //randomize the drop zone and set ring stack

    [SerializeField] private Timer timer;

    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0; // VSync must be disabled.
        Application.targetFrameRate = 60;
#endif
    }

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

        for(int i = 0; i < ringOP.inventorySize; i++)
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

        for(int i = 0; ringOP.inventory.cache > 0; i++)
        {            
            OutsideRingStacks[i % 2].inventory.AddToInventory(ringOP.inventory.PopFromInventory());
        }
    }

    private void EndGame()
    {
        if(scoringSystem.BlueTeam > scoringSystem.RedTeam)
        {
            alternateColor = img_blueteam.color;
            target_img = img_blueteam;
            InvokeRepeating("FlashGreen", 0, 1f);
        }
        else if (scoringSystem.BlueTeam < scoringSystem.RedTeam)
        {
            alternateColor = img_redteam.color;
            target_img = img_redteam;
            InvokeRepeating("FlashGreen", 0, 1f);
        }

        restart_btn.SetActive(true);
    }


    bool isGreen = false;
    Color alternateColor;
    Image target_img;
    private void FlashGreen()
    {
        if (isGreen)
            target_img.color = Color.green;
        else
            target_img.color = alternateColor;

        isGreen = !isGreen;
    }
    
    
    IEnumerator StartCountingDropZones(DropZoneTrigger[] zones, bool isEndGame)
    {
        if (zones[0] is DropZoneTrigger)
        {
            foreach (DropZoneTrigger drops in zones)
            {
                if (drops.isActivated)
                    drops.isCountingWobblers = true;
            }
        }

        yield return new WaitForFixedUpdate();

        
        foreach (DropZoneTrigger drops in DropZones)
        {
            if (drops.isActivated)
                drops.isCountingWobblers = false;
        }

        if (isEndGame)
            EndGame();

        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetRingShots(bool isActive)
    {
        foreach(RingShotController r in RingShots)
        {
            r.Reset();
            r.isActivated = isActive;
        }
    }

    private void SetupNewRules()
    {
        if (currentRuleIndex == 1)
        {
            StartCoroutine(StartCountingDropZones(DropZones, false));
            ResetRingShots(false);
        }
        else if (currentRuleIndex == 2)
        {
            ResetRingShots(true);
        }
        if (currentRuleIndex >= GameModeTimeIntervals.Length)
        {
            StopCoroutine("StartCountingDropZones");
            StartCoroutine(StartCountingDropZones(EndGameDropZones, false));
            StartCoroutine(StartCountingDropZones(StartLines, true));
            return;
        }

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
