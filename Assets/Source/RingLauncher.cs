using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class RingLauncher : MonoBehaviour
{
    [SerializeField] private float flywheelPower = 5f;    
    [SerializeField] private int inventory = 0;
    [SerializeField] private RingCollectorController ringCollecter;
    [SerializeField] GameObject launcherDirection;    

    [SerializeField] private GameObject[] attachmentPoints;
    private Queue<GameObject> ringQueue;

    private void Start()
    {
        ringQueue = new Queue<GameObject>();
        //ringCollecter = GetComponentInChildren<RingCollectorController>();
        Reset();
    }

    private void Reset()
    {
        inventory = 0;
        ringQueue.Clear();
    }

    private void SetFlyWheelPowerTo(float value)
    {
        flywheelPower = value;
    }

    public void LaunchRing()
    {
        // check ring count
        if(inventory > 0)
        {
            // launch ring using current Flywheel Power as force
            var nextRing = ringQueue.Dequeue();
            nextRing.transform.parent = null;
            //launch in direction the launcher is facing
            var rb = nextRing.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.mass = rb.GetComponent<ScoringObjectDataContainer>().data.Mass;
            rb.AddForce(launcherDirection.transform.forward * flywheelPower);
            inventory--;
        }        
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
        var rb = newRing.GetComponent<Rigidbody>();
        rb.isKinematic = true;        
        rb.mass = 0;

        //add to attachment point
        newRing.transform.SetParent(attachmentPoints[inventory].transform, false);
        newRing.transform.localPosition = Vector3.zero;
        inventory++;
        ringQueue.Enqueue(newRing);
        
    }
}
