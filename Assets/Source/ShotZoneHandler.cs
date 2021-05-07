using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotZoneHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //no shots for you!
            other.GetComponentInParent<RingLauncher>().isInShotZone = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //no shots for you!
            other.GetComponentInParent<RingLauncher>().isInShotZone = true;
        }
    }
}
