using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour
{
    MovementActions movement;
    LauncherActions ringLauncherAction;
    MovementController movementController;

    RingLauncher ringLauncherController;

    private void Start()
    {
        movementController = GetComponent<MovementController>();
        movement = new MovementActions();

        movement.Forward.AddDefaultBinding(InputControlType.LeftStickY);
        movement.Strafe.AddDefaultBinding(InputControlType.LeftStickX);
        movement.RotatePosX.AddDefaultBinding(InputControlType.RightStickX);
        movement.RotatePosY.AddDefaultBinding(InputControlType.RightStickY);

        ringLauncherController = GetComponent<RingLauncher>();
        ringLauncherAction = new LauncherActions();
        ringLauncherAction.ActivateRingIntake.AddDefaultBinding(InputControlType.Action1);
    }

    private void Update()
    {
        if(movement.Forward.IsPressed)
        {
            movementController.AccelerateBy(percent: movement.Forward.Value);
        }

        if (ringLauncherAction.ActivateRingIntake.IsPressed)
        {
            ringLauncherController.ActivateRingIntake();
        }

        movementController.Rotate(movement.RotatePosX.Value, movement.RotatePosY.Value);
    }
}

