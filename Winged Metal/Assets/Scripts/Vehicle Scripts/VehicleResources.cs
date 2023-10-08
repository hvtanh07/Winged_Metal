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
    protected float tankCurrentEnergy;
    protected float lastEnergyUsedTime;
    public int energySupply;


    protected void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;
    }
    void OnEnable()
    {

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
            vehicle.ID.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
            if (tankCurrentEnergy > tankMaxEnergy)
                tankCurrentEnergy = tankMaxEnergy;
        }
        if (tankCurrentArmor < tankMaxArmor && Time.time - lastDamageTime > 5.0f)
        {
            tankCurrentArmor += recoveryEfficiency * Time.deltaTime;
            //vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
            if (tankCurrentArmor > tankMaxArmor)
                tankCurrentArmor = tankMaxArmor;
        }
    }
    public void InitiateParameter(int maxArmor, int maxEnergy)
    {
        tankMaxArmor = maxArmor;
        tankMaxEnergy = maxEnergy;
        tankCurrentArmor = tankMaxArmor;
        tankCurrentEnergy = tankMaxEnergy;
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
        vehicle.ID.events.OnEnUpdate?.Invoke(tankCurrentEnergy);
        vehicle.ID.events.OnBeingHit?.Invoke();
        lastDamageTime = Time.time;
        BehaviourTreeRunner bt = GetComponent<BehaviourTreeRunner>();
        if (bt != null)
        {
            GetComponent<BehaviourTreeRunner>().tree.blackboard.beingHit = true;
        }


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