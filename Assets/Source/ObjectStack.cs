using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStack : MonoBehaviour
{
    [HideInInspector] public int cache = 0;
    [SerializeField] private GameObject[] attachmentPoints;
    public int maxSize = 0;

    public void Start()
    {
        cache = 0;
        maxSize = attachmentPoints.Length;
    }

    public void AddToStack(GameObject newItem)
    {
        if (cache < maxSize)
        {
            Debug.Log("ring collected");
            //add to attachment point
            int newIndex = cache;
            newItem.transform.parent = null; //unparent ring to move freely in arena
            newItem.transform.position = attachmentPoints[newIndex].transform.position;
            newItem.transform.rotation = attachmentPoints[newIndex].transform.rotation;            
            
            // reactivate the physics 
            var rb = newItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.mass = rb.GetComponent<ScoringObjectDataContainer>().data.Mass;
            newItem.GetComponentInChildren<Collider>().enabled = true;

            Debug.Break();
            cache++;
        }
    }
}
