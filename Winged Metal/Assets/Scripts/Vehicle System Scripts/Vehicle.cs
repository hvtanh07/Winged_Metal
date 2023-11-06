using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VehicleSide
{
    player,
    enemy,
    ally
}


public class Vehicle : MonoBehaviour
{
    public VehicleID ID;
    public VehicleSide side;
}


