using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class RingLauncher : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject[] attachmentPoints;
    [SerializeField] private float flywheelPower = 5f;
    [SerializeField] private RingCollectorController ringCollecter;
    [SerializeField] GameObject launcherDirection;
    public int inventorySize = 0;

    private void Start()
    {
        inventory = new Inventory(size: inventorySize, attachmentPoints);        
        Reset();
    }

    private void Reset()
    {
        inventory = new Inventory(size: inventorySize, attachmentPoints);
    }

    private void SetFlyWheelPowerTo(float value)
    {
        flywheelPower = value;
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
            nextRing.GetComponentInChildren<Collider>().enabled = false;
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

[System.Serializable]
public class Inventory
{
    [HideInInspector] public int cache = 0;
    private GameObject[] attachmentPoints;
    public Queue<GameObject> itemQueue;
    private int maxSize = 0;

    public Inventory(int size)
    {
        itemQueue = new Queue<GameObject>();
        cache = 0;
        maxSize = size;
    }

    public Inventory(int size, GameObject[] attachPoints)
    {
        itemQueue = new Queue<GameObject>();
        cache = 0;
        maxSize = size;

        attachmentPoints = new GameObject[attachPoints.Length];
        for(int i = 0; i< attachPoints.Length; i++)
        {
            attachmentPoints[i] = attachPoints[i];
        }
    }

    public void AddToInventory(GameObject newItem)
    {
        if(cache < maxSize)
        {
            Debug.Log("ring collected");
            var rb = newItem.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.mass = 0;
            newItem.GetComponentInChildren<Collider>().enabled = false;

            //add to attachment point
            int newIndex = cache % attachmentPoints.Length;
            Debug.Log("new index: " + newIndex);
            newItem.transform.SetParent(attachmentPoints[newIndex].transform, false);//TODO: loop attachmentpoints if != to maxSize

            

            newItem.transform.localPosition = Vector3.zero;
            cache++;
            itemQueue.Enqueue(newItem);        
        }
    }

    public GameObject PopFromInventory()
    {
        cache--;
        return itemQueue.Dequeue();
    }
}
