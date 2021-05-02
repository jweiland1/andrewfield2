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
        Debug.Log("MoveForward :" + percent);
        float acceleration = (topSpeed * conversionRate) * percent;
        transform.position += acceleration * pivotPoint.forward *-1 * Time.deltaTime;
    }

    public void Rotate(float x, float y)
    {
        //Debug.Log("x : " + x + " | y : " + y);

        Vector3 newRot = new Vector3(x, 0, y);

        var currentPos = rb.position;

        var facePos = currentPos + newRot;

        pivotPoint.LookAt(facePos);
    }
}
