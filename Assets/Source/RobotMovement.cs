using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class RobotMovement : PlayerActionSet
{
    public PlayerAction Forward;
    public PlayerAction Strafe;
    public PlayerAction RotatePosX, RotatePosY;
    public PlayerOneAxisAction Rotate;

    public RobotMovement()
    {
        Forward = CreatePlayerAction("Move Forward");
        Strafe = CreatePlayerAction("Strafe");
        RotatePosX = CreatePlayerAction("Rotate Left");
        RotatePosY = CreatePlayerAction("Rotate Right");
        Rotate = CreateOneAxisPlayerAction(RotatePosX, RotatePosY);
    }
}