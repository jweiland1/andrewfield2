using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class RingLauncher : MonoBehaviour
{    
    private float flywheelPower = 0f;    
    [SerializeField] private int inventory = 0;
    RingCollectorController ringCollecter;

    [SerializeField] private GameObject[] attachmentPoints;

    private void Start()
    {   
        ringCollecter = GetComponentInChildren<RingCollectorController>();
        Reset();
    }

    private void Reset()
    {
        inventory = 0;
    }

    private void SetFlyWheelPowerTo(float value)
    {
        flywheelPower = value;
    }

    private void LaunchRing()
    {
        // check ring count
        // if > 0
            // launch ring using current Flywheel Power as force
    }

    private void SpinFlyWheel()
    {
        //doesn't need to spin        
    }

    public void ActivateRingIntake()
    {
        // check if robot can collect more rings
        if (inventory < 3)
        {
            //check for if ring is touching collector
            if (ringCollecter.IsRingCollectible())
            {
                AddRingToInventory(ringCollecter.CollectRing());
            }
        }

        //spin the thingies (do later)
    }

    private void AddRingToInventory(GameObject newRing)
    {
        Debug.Log("ring collected");
        newRing.GetComponent<Rigidbody>().isKinematic = true;
        newRing.transform.SetParent(attachmentPoints[inventory].transform, false);
        newRing.transform.localPosition = Vector3.zero;
        inventory++;
        //add to attachment point
    }
}
