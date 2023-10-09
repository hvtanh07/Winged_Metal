using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.UI;

public class VehicleResources : VehicleSystem
{
    //Armor
    public int tankMaxArmor;
    [SerializeField]
    protected float tankCurrentArmor;
    protected float lastDamageTime;
    public VehicleAttack.BulletOwner[] damageDealers;
    public int recoveryEfficiency;

    //Energy
    public int tankMaxEnergy;
    [SerializeField]
    protected float tankCurrentEnergy;
    protected float lastEnergyUsedTime;
    public int energySupply;


    protected void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;
        vehicle.ID.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
    }
    void OnEnable()
    {
        vehicle.ID.events.OnEnUsed += ConsumeEnergy;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        BulletScript bullet = col.gameObject.GetComponent<BulletScript>();
        if (bullet == null) return; //if object is not bullet then no need to do anything
        foreach (VehicleAttack.BulletOwner damageDealer in damageDealers)
        {
            if (damageDealer == bullet.bulletOwner)
            {
                TakeDamage(bullet.GetDamage());
                col.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void Update()
    {
        if (tankCurrentEnergy < tankMaxEnergy && Time.time - lastEnergyUsedTime > 3.0f)
        {
            tankCurrentEnergy += energySupply * Time.deltaTime;
            if (tankCurrentEnergy > tankMaxEnergy)
                tankCurrentEnergy = tankMaxEnergy;
            vehicle.ID.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        }
        if (tankCurrentArmor < tankMaxArmor && Time.time - lastDamageTime > 5.0f)
        {
            tankCurrentArmor += recoveryEfficiency * Time.deltaTime;
            if (tankCurrentArmor > tankMaxArmor)
                tankCurrentArmor = tankMaxArmor;
            vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
        }
    }
    public void InitiateParameter(int maxArmor, int maxEnergy)
    {
        tankMaxArmor = maxArmor;
        tankMaxEnergy = maxEnergy;
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;
        vehicle.ID.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
    }

    public void ConsumeEnergy(int amountEnergyComsumned)
    {
        if (amountEnergyComsumned > tankCurrentEnergy) return; //insufficient energy 
        tankCurrentEnergy -= amountEnergyComsumned;
        vehicle.ID.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        lastEnergyUsedTime = Time.time;
    }
    public void TakeDamage(int damage)
    {
        tankCurrentArmor -= damage;
        vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
        vehicle.ID.events.OnBeingHit?.Invoke();
        lastDamageTime = Time.time;

        if (tankCurrentArmor <= 0)
        {
            //death
        }
    }
    public float GetCurrentEn()
    {
        return tankCurrentEnergy;
    }
    public float GetCurrentArmor()
    {
        return tankCurrentArmor;
    }



}
