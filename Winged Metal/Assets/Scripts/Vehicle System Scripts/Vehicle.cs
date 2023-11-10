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
    //public VehicleID ID;
    public VehicleEvents events;
    public int vehicleWeight;
    public int vehicleArmor;
    public VehicleSide side;
}


