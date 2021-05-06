using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] private GameObject[] attachmentPoints;
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
}
