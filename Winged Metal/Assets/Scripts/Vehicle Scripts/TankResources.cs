using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankResources : MonoBehaviour
{
    public int tankMaxArmor;
    private float tankCurrentArmor;
    public int recoveryEfficiency;
    private float lastDamageTime;

    public int tankMaxEnergy;
    private float tankCurrentEnergy;
    public int energySupply;
    private float lastEnergyUsedTime;

    public Slider slider;

    private void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;

        slider.maxValue = tankMaxEnergy;
    }
    public void InitiateParameter(int maxArmor, int maxEnergy)
    {
        tankMaxArmor = maxArmor;
        tankMaxEnergy = maxEnergy;
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;

        slider.maxValue = tankMaxEnergy;
    }

    public bool ConsumeEnergy(int amountEnergyComsumned)
    {
        if (amountEnergyComsumned > tankCurrentEnergy) return false; //insufficient energy 
        tankCurrentEnergy -= amountEnergyComsumned;
        lastEnergyUsedTime = Time.time;
        return true;
    }

    public void TakeDamage(int damage)
    {
        tankCurrentArmor -= damage;
        lastDamageTime = Time.time;

        if (tankCurrentArmor <= 0)
        {
            //death
        }
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
