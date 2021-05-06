using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingerFlinger : MonoBehaviour
{
    [SerializeField] private int inventorySize = 20;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject[] attachmentPoints;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(size: inventorySize, attachmentPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
