using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblerAttachmentController : MonoBehaviour
{
    public int maxInventory = 0;
    [SerializeField] private int inventory = 0;
    [SerializeField] private RingCollectorController ringCollecter;
    [SerializeField] private GameObject attachmentPoint;
    private Queue<GameObject> ringQueue;
    private float inventoryMass = 0f;
    private Animator anim;

    private void Start()
    {
        attachmentPoint = this.gameObject;
        ringQueue = new Queue<GameObject>();
        anim = GetComponent<Animator>();
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
            var rb = nextRing.GetComponentInChildren<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.mass = inventoryMass;            
            inventory--;
            var cols = nextRing.GetComponentsInChildren<Collider>();
            foreach (Collider c in cols)
            {
                c.enabled = true;
            }
            anim.SetTrigger("Drop");
        }
    }

    private void AddObjectToInventory(GameObject newRing)
    {
        Debug.Log("wobbler collected");
        var rb = newRing.GetComponentInChildren<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
        inventoryMass = rb.mass;

        
        rb.mass = 0;
        //add to attachment point
        newRing.transform.SetParent(attachmentPoint.transform, false);
        newRing.transform.localPosition = Vector3.zero;
        inventory++;
        ringQueue.Enqueue(newRing);
        anim.SetTrigger("Lift");
    }
}
