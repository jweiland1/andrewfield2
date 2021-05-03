using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblerJobbler : MonoBehaviour
{
    public Vector3 centerOfMass;
    public Rigidbody rb;

    void Start()
    {        
        rb.centerOfMass = centerOfMass;
    }

    private void Update()
    {
        rb.centerOfMass = centerOfMass;
    }
}
