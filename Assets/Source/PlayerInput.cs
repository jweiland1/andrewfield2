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

    WobblerArmActions wobblerAction;
    WobblerAttachmentController wobblerController;

    public float dpadSensitivity = 5f;

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
        ringLauncherAction.LaunchRing.AddDefaultBinding(InputControlType.LeftTrigger);
        ringLauncherAction.IncreaseFlywheelPower.AddDefaultBinding(InputControlType.DPadUp);
        ringLauncherAction.DecreaseFlywheelPower.AddDefaultBinding(InputControlType.DPadDown);

        wobblerAction = new WobblerArmActions();
        wobblerController = GetComponentInChildren<WobblerAttachmentController>();
        wobblerAction.LiftArm.AddDefaultBinding(InputControlType.RightBumper);
        wobblerAction.LowerArm.AddDefaultBinding(InputControlType.LeftBumper);
    }

    private void Update()
    {
        if (ringLauncherAction.ActivateRingIntake.IsPressed)
        {
            ringLauncherController.ActivateRingIntake();
        }

        if (ringLauncherAction.LaunchRing.WasPressed)
        {
            ringLauncherController.LaunchRing();
        }

        if(wobblerAction.LiftArm.WasPressed)
        {
            wobblerController.GrabWobbler();
        }

        if (wobblerAction.LowerArm.WasPressed)
        {
            wobblerController.DropWobbler();
        }

        if (ringLauncherAction.IncreaseFlywheelPower.IsPressed)
        {
            ringLauncherController.AdjustFlyWheelPowerBy(dpadSensitivity * Time.deltaTime);
        }

        if (ringLauncherAction.DecreaseFlywheelPower.IsPressed)
        {
            ringLauncherController.AdjustFlyWheelPowerBy(-dpadSensitivity * Time.deltaTime);
        }

        movementController.Movement(movement.Forward.Value, movement.Strafe.Value);
        movementController.Rotate(movement.RotatePosX.Value, movement.RotatePosY.Value);
    }
}

