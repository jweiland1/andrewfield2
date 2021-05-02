using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MovementActions : PlayerActionSet
{
    public PlayerAction Forward;
    public PlayerAction Strafe;
    public PlayerAction RotatePosX, RotatePosY;
    public PlayerOneAxisAction Rotate;

    public MovementActions()
    {
        Forward = CreatePlayerAction("Move Forward");
        Strafe = CreatePlayerAction("Strafe");
        RotatePosX = CreatePlayerAction("Rotate Left");
        RotatePosY = CreatePlayerAction("Rotate Right");
        Rotate = CreateOneAxisPlayerAction(RotatePosX, RotatePosY);
    }
}

public class LauncherActions : PlayerActionSet
{
    public PlayerAction IncreaseFlywheelPower;
    public PlayerAction DecreaseFlywheelPower;
    public PlayerAction LaunchRing;
    public PlayerAction SpinFlywheel;
    public PlayerAction ActivateRingIntake;

    public LauncherActions()
    {
        IncreaseFlywheelPower = CreatePlayerAction("Increase Flywheel Power");
        DecreaseFlywheelPower = CreatePlayerAction("Decrease Flywheel Power");

        LaunchRing = CreatePlayerAction("LaunchRing");
        SpinFlywheel = CreatePlayerAction("Spin Flywheel");
        ActivateRingIntake = CreatePlayerAction("Activate Ring Intake");
    }
}

public class WobblerArmActions : PlayerActionSet
{
    public PlayerAction LiftArm, LowerArm;

    public WobblerArmActions()
    {
        LiftArm = CreatePlayerAction("Lift Arm");
        LowerArm = CreatePlayerAction("Lower Arm");
    }
}
