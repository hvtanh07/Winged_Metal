using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleEvents
{
    //radar
    public Action<List <Transform>> OnTargetDetected; 
    //attack
    public Action<Vector2, bool> OnAttackDirectionChange;
    public Action<VehicleSide> OnVehicleSideAssigned;
  
    //movement
    public Action<Vector2> OnDirectionChange;
    public Action<Vector2> OnDashCalled;
    public Action OnDashComplete;

    //energy
    public Action<float> OnEnUpdate;
    public Action<int> OnEnUsed;

    //armor
    public Action<float> OnArmorUpdate;
    public Action<Vector2> OnBeingHit;

    //second weapon
    public Action<Transform[]> On2ndAttackCalled;
}
