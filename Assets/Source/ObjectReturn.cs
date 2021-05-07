using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    public InventoryContainer [] stacks;
    int index = 0;
    

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ring"))
        {
            if (index > 1)
                index = 0;

            stacks[index].inventory.AddToInventory(other.transform.parent.gameObject);
            index++;
        }
    }
}
