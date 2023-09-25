using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankResources : TankResources
{
    public int recoveryEfficiency;

    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (tankCurrentArmor < tankMaxArmor && Time.time - lastDamageTime > 5.0f)
        {
            tankCurrentArmor += recoveryEfficiency * Time.deltaTime;
            if (tankCurrentArmor > tankMaxArmor)
                tankCurrentArmor = tankMaxArmor;
        }

        if (tankCurrentEnergy < tankMaxEnergy && Time.time - lastEnergyUsedTime > 3.0f)
        {
            tankCurrentEnergy += energySupply * Time.deltaTime;

            if (tankCurrentEnergy > tankMaxEnergy)
                tankCurrentEnergy = tankMaxEnergy;
        }

        slider.value = tankCurrentEnergy;
    }

}
