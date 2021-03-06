using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class RingLauncher : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject[] attachmentPoints;
    [SerializeField] public float flywheelMaxPower = 5f;
    [SerializeField] public float flywheelPower = 5f;
    [SerializeField] private RingCollectorController ringCollecter;
    [SerializeField] GameObject launcherDirection;
    public int inventorySize = 0;
    public bool isInShotZone = false;

    public delegate void InputEvent(float value);
    public event InputEvent OnPowerChanged;

    private void Start()
    {
        inventory = new Inventory(size: inventorySize, attachmentPoints);        
        Reset();
    }

    private void Reset()
    {
        inventory = new Inventory(size: inventorySize, attachmentPoints);
    }

    public void AdjustFlyWheelPowerBy(float value)
    {
        flywheelPower += value;
        OnPowerChanged?.Invoke(value);

    }

    public void LaunchRing()
    {
        // check ring count
        if(inventory.cache > 0)
        {
            // launch ring using current Flywheel Power as force
            var nextRing = inventory.PopFromInventory();
            nextRing.transform.parent = null;
            //launch in direction the launcher is facing
            var rb = nextRing.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.mass = rb.GetComponent<ScoringObjectDataContainer>().data.Mass;
            rb.GetComponent<ScoringObjectDataContainer>().isValidShot = isInShotZone;
            nextRing.GetComponentInChildren<Collider>().enabled = true;
            rb.AddForce(launcherDirection.transform.forward * flywheelPower);            
        }
    }

    private void SpinFlyWheel()
    {
        //doesn't need to spin        
    }

    public void ActivateRingIntake()
    {
        
        //check for if ring is touching collector
        if (ringCollecter.IsRingCollectible())
        {
            inventory.AddToInventory(ringCollecter.CollectRing());
        }

        //spin the thingies (do later)
    }
}

