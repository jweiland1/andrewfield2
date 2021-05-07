using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollectorController : MonoBehaviour
{
    RingLauncher ringLauncher;
    List<GameObject> ringPool;
    public int poolCount = 0;
    [SerializeField] private string tag = "";

    private void Start()
    {
        ringLauncher = GetComponentInParent<RingLauncher>();
        ringPool = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            if (other.transform.CompareTag(tag))
            {                
                ringPool.Add(other.transform.parent.gameObject);
                poolCount++;                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent)
        {
            if(other.transform.CompareTag(tag))
            {
                ringPool.Remove(other.transform.parent.gameObject);
                poolCount--;
            }
        }
    }

    public bool IsRingCollectible()
    {
        return ringPool.Count > 0;
    }

    public GameObject CollectRing()
    {
        GameObject retVal = ringPool[0];        
        ringPool.RemoveAt(0);
        poolCount--;        
        return retVal;
    }

}
