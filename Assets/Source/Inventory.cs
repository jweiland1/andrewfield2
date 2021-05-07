using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory
{
    [HideInInspector] public int cache = 0;
    private GameObject[] attachmentPoints;
    public Queue<GameObject> itemQueue;
    public int maxSize = 0;
    private bool hasAttachmenPoints = false;

    public Inventory(int size)
    {
        itemQueue = new Queue<GameObject>();
        cache = 0;
        maxSize = size;
        hasAttachmenPoints = false;
    }

    public Inventory(int size, GameObject[] attachPoints)
    {
        itemQueue = new Queue<GameObject>();
        cache = 0;
        maxSize = size;
        hasAttachmenPoints = true;

        attachmentPoints = new GameObject[attachPoints.Length];
        for (int i = 0; i < attachPoints.Length; i++)
        {
            attachmentPoints[i] = attachPoints[i];
        }
    }

    public void AddToInventory(GameObject newItem)
    {
        if (cache < maxSize)
        {
            Debug.Log("ring collected");
            var rb = newItem.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.mass = 0;
            newItem.GetComponentInChildren<Collider>().enabled = false;

            if (hasAttachmenPoints)
            {
                //add to attachment point
                int newIndex = cache % attachmentPoints.Length;
                //Debug.Log("new index: " + newIndex);
                newItem.transform.SetParent(attachmentPoints[newIndex].transform, false);//TODO: loop attachmentpoints if != to maxSize
                newItem.transform.localPosition = Vector3.zero;
                newItem.transform.rotation = Quaternion.identity;
            }
            else
            {
                // TODO: hidden cache later
            }

            cache++;
            itemQueue.Enqueue(newItem);
        }
    }

    public GameObject PopFromInventory()
    {
        if (cache > 0)
        {
            cache--;
            return itemQueue.Dequeue();
        }
        else
            return null;
    }
}
