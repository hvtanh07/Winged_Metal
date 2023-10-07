using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleEvents
{
    //attack
    public Action<Vector2> OnAttackDirectionChange;

    //movement
    public Action<Vector2> OnMovementDirectionChange;
    public Action<Vector2> OnDash;


}
