using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankResources : MonoBehaviour
{
    //Armor
    public int tankMaxArmor;
    protected float tankCurrentArmor;
    protected float lastDamageTime;


    //Energy
    public int tankMaxEnergy;
    protected float tankCurrentEnergy;
    protected float lastEnergyUsedTime;

    public Slider slider;

    private void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;

        if(slider != null)
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

    

}
