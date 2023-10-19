using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleEvents
{
    //ai
    public Action<Transform[]> OnTargetDetected; 
    //attack
    public Action<Vector2, bool> OnAttackDirectionChange;
  
    //movement
    public Action<Vector2> OnDirectionChange;
    public Action<Vector2> OnDashCalled;
    public Action OnDashComplete;

    //resource
    public Action<float> OnEnUpdate;
    public Action<int> OnEnUsed;
    public Action<float> OnArmorUpdate;
    public Action<int> OnTakeHit;
    public Action OnBeingHit;

    //second weapon
    public Action<Transform[]> On2ndAttackCalled;
}
