using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblerAttachmentController : MonoBehaviour
{
    public int maxInventory = 0;
    [SerializeField] private int inventory = 0;
    RingCollectorController ringCollecter;
    [SerializeField] private GameObject attachmentPoint;
    private Queue<GameObject> ringQueue;

    private void Start()
    {
        attachmentPoint = this.gameObject;
    }

    private void Reset()
    {
        inventory = 0;        
    }

    public void GrabWobbler()
    {
        // check if robot can collect more rings
        if (inventory < maxInventory)
        {
            //check for if ring is touching collector
            if (ringCollecter.IsRingCollectible())
            {
                AddObjectToInventory(ringCollecter.CollectRing());
            }
        }
    }

    public void DropWobbler()
    {
        // check ring count
        if (inventory > 0)
        {
            // launch ring using current Flywheel Power as force
            var nextRing = ringQueue.Dequeue();
            nextRing.transform.parent = null;
            //launch in direction the launcher is facing
            nextRing.GetComponentInChildren<Rigidbody>().isKinematic = false;            
            inventory--;
        }
    }

    private void AddObjectToInventory(GameObject newRing)
    {
        Debug.Log("wobbler collected");
        newRing.GetComponentInChildren<Rigidbody>().isKinematic = true;

        //add to attachment point
        newRing.transform.SetParent(attachmentPoint.transform, false);
        newRing.transform.localPosition = Vector3.zero;
        inventory++;
        ringQueue.Enqueue(newRing);
    }


}
