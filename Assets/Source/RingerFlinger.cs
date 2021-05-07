using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingerFlinger : MonoBehaviour
{    
    [SerializeField] private InventoryContainer inventoryContainer;
    [SerializeField] GameObject launcherDirection;
    [SerializeField] private float launchPower = 5f;
    [SerializeField] private float launchRange = 5f;
    [SerializeField] private float angleRange = 5f;

    public void LaunchObject()
    {
        // check ring count
        if (inventoryContainer.inventory.cache > 0)
        {
            // launch ring using current Flywheel Power as force
            var nextObject = inventoryContainer.inventory.PopFromInventory();
            if(nextObject)
            {
                nextObject.transform.parent = null;
                nextObject.transform.position = launcherDirection.transform.position;
                nextObject.transform.rotation = launcherDirection.transform.rotation;
                //launch in direction the launcher is facing
                var rb = nextObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.mass = rb.GetComponent<ScoringObjectDataContainer>().data.Mass;
                nextObject.GetComponentInChildren<Collider>().enabled = true;

                float randomLaunchPower = Random.Range(launchPower - launchRange, launchPower + launchRange);
                Vector3 randomAngle = new Vector3(
                    Random.Range(launcherDirection.transform.forward.x - angleRange, launcherDirection.transform.forward.x + angleRange),
                    Random.Range(launcherDirection.transform.forward.y - angleRange, launcherDirection.transform.forward.y + angleRange),
                    Random.Range(launcherDirection.transform.forward.z - angleRange, launcherDirection.transform.forward.z + angleRange)
                    );

                rb.AddForce(randomAngle * randomLaunchPower);
            }
        }            
    }
}
