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
        Init();
    }

    private void Init()
    {
        if (attachmentPoints.Length > 0)
            inventory = new Inventory(size: inventorySize, attachmentPoints);
        else
            inventory = new Inventory(size: inventorySize);
    }

    private void Reset()
    {
        Init();
    }
}
