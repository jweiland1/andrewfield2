using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblerJobbler : MonoBehaviour
{
    public Vector3 centerOfMass;
    public Rigidbody rb;
    public int id = 0;

    void Start()
    {        
        rb.centerOfMass = centerOfMass;
    }

    private void Update()
    {
        rb.centerOfMass = centerOfMass;
    }
}
