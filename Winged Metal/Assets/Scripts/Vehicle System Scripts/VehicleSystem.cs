using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleSystem : MonoBehaviour
{
    protected Vehicle vehicle;
    public int unitWeight;
    public int unitArmor;
    
    
    protected virtual void Awake(){  
        vehicle = transform.root.GetComponent<Vehicle>();
        vehicle.vehicleWeight += unitWeight;
        vehicle.vehicleArmor += unitArmor;
    }
}
