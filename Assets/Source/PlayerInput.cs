using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour
{
    RobotMovement robotMovement;
    MovementController movementController;

    private void Start()
    {
        movementController = GetComponent<MovementController>();
        robotMovement = new RobotMovement();

        robotMovement.Forward.AddDefaultBinding(InputControlType.LeftStickY);
        robotMovement.Strafe.AddDefaultBinding(InputControlType.LeftStickX);
        robotMovement.RotatePosX.AddDefaultBinding(InputControlType.RightStickX);
        robotMovement.RotatePosY.AddDefaultBinding(InputControlType.RightStickY);
    }

    private void Update()
    {
        if(robotMovement.Forward.IsPressed)
        {
            movementController.MoveForward(robotMovement.Forward.Value);
        }

        movementController.Rotate(robotMovement.RotatePosX.Value, robotMovement.RotatePosY.Value);
    }
}

