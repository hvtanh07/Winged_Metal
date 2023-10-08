using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleEvents
{
    //attack
    public Action<Vector2> OnAttackDirectionChange;
  
    //movement
    public Action<Vector2> OnDirectionChange;
    public Action<Vector2> OnDashCalled;

    //resource
    public Action<float> OnEnUpdate;
    public Action<float> OnArmorUpdate;
    public Action OnBeingHit;
}
