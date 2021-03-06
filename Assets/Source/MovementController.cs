using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{    
    private Rigidbody rb;
    [SerializeField] private float movementSensitivity = 5.0f;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private float topSpeed = 10;

    private const float conversionRate = 0.3048f; // converting meters per sec to feet per sec

    private void Start()
    {
        rb = pivotPoint.GetComponent<Rigidbody>();
    }

    ///<summary>Accelerate by a percent of TopSpeed</summary>
    public void AccelerateBy(float percent)
    {
        //Debug.Log("MoveForward :" + percent);
        float acceleration = (topSpeed) * Mathf.Abs(percent);
        
        if(percent > 0)
            rb.AddForce(-pivotPoint.forward * acceleration, ForceMode.Force);
        else
            rb.AddForce(pivotPoint.forward * acceleration, ForceMode.Force);
    }

    ///<summary>Rotate Towards the Right Stick vector direction</summary>
    public void Rotate(float x, float y)
    {
        //Debug.Log("x : " + x + " | y : " + y);

        Vector3 newRot = new Vector3(x, 0, y);
        var currentPos = rb.position;
        var facePos = currentPos + newRot;
        pivotPoint.LookAt(facePos);
    }

    ///<summary>Left  Stick Movement based on the current camera position</summary>
    public void Movement(float x, float y)
    {
        //Debug.Log("x : " + x + " | y : " + y);
        Vector3 newRot = new Vector3(-y, 0, -x);

        //Debug.Log("facePOS : " + newRot.ToString());
        Debug.DrawRay(rb.position, newRot * 10, Color.green);
        rb.AddForce(newRot * topSpeed);
    }
}
