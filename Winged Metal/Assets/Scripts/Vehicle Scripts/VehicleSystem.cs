using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleSystem : MonoBehaviour
{
    protected Vehicle vehicle;
    protected virtual void Awake(){
        vehicle = transform.root.GetComponent<Vehicle>();
    }
}
