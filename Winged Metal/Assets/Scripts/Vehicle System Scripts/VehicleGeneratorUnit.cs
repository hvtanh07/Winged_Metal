using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.UI;

public class VehicleGeneratorUnit : VehicleSystem
{
    //Energy
    public int tankMaxEnergy;
    [SerializeField]
    protected float tankCurrentEnergy;
    protected float lastEnergyUsedTime;
    public int energySupply;


    protected void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        tankCurrentEnergy = tankMaxEnergy;
        vehicle.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
    }
    void OnEnable()
    {
        vehicle.events.OnEnUsed += ConsumeEnergy;
    }


    private void Update()
    {
        if (tankCurrentEnergy < tankMaxEnergy && Time.time - lastEnergyUsedTime > 1f)
        {
            tankCurrentEnergy += energySupply * Time.deltaTime;
            if (tankCurrentEnergy > tankMaxEnergy)
                tankCurrentEnergy = tankMaxEnergy;
            vehicle.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        }
    }
    public void InitiateParameter(int maxEnergy)
    {

        tankMaxEnergy = maxEnergy;
        tankCurrentEnergy = tankMaxEnergy;
        vehicle.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
    }

    public void ConsumeEnergy(int amountEnergyComsumned)
    {
        if (amountEnergyComsumned > tankCurrentEnergy) return; //insufficient energy 
        tankCurrentEnergy -= amountEnergyComsumned;
        vehicle.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        lastEnergyUsedTime = Time.time;
    }

    public float GetCurrentEn()
    {
        return tankCurrentEnergy;
    }
}
