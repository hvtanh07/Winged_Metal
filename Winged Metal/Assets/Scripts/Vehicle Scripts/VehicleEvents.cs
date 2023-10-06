using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleEvents
{
    //attack
    public Action<int> GetEntoDash;
    public Action CallToDash;
    //movement
    public Action<Vector2> OnDash;
    public Action<int> GetEntoShoot;
    public Action CallToShoot;

    public Action<Vector2> OnDirectionChange;
    //resource
    public Action En;
}
