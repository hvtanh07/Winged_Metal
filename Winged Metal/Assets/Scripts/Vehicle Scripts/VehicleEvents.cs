using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleEvents
{
    //attack
    public Action<Vector2> OnAttackDirectionChange;
    
    public Action<int> GetEntoShoot;
    public Action CallToDash;

    
    //movement
    public Action<Vector2> OnDirectionChange;
    public Action<Vector2> OnDash;
    public Action<int> GetEntoDash;
    public Action CallToShoot;

    
    //resource
    public Action En;
}
