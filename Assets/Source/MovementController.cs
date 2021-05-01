using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Vector3 newMovement;
    private Rigidbody rb;
    [SerializeField] private float movementSensitivity = 5.0f;
    [SerializeField] private Transform pivotPoint;

    private void Start()
    {
        rb = pivotPoint.GetComponent<Rigidbody>();
    }

    public void MoveForward(float value)
    {
        Debug.Log("MoveForward :" + value);
        newMovement = new Vector3(0,0,value/movementSensitivity);
        transform.position += newMovement;
    }

    public void Rotate(float x, float y)
    {
        Debug.Log("x : " + x + " | y : " + y);

        newMovement = new Vector3(x, 0, y);

        var currentPos = rb.position;

        var facePos = currentPos + newMovement;

        pivotPoint.LookAt(facePos);
    }
}
