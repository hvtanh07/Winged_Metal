using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public VehicleID ID;

    //Armor
    public int maxArmor;
    [SerializeField]
    public float currentArmor;
    protected float lastDamageTime;
    public int recoveryEfficiency;
    public VehicleAttack.BulletOwner[] damageDealers;

    //Energy
    public int maxEnergy;
    public float currentEnergy;
    protected float lastEnergyUsedTime;
    public int energySupply;

    protected void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        currentArmor = maxArmor;
        currentEnergy = maxEnergy;
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
        if (currentEnergy < maxEnergy && Time.time - lastEnergyUsedTime > 3.0f)
        {
            currentEnergy += energySupply * Time.deltaTime;
            if (currentEnergy > maxEnergy)
                currentEnergy = maxEnergy;
        }

        if (currentArmor < maxArmor && Time.time - lastDamageTime > 5.0f)
        {
            currentArmor += recoveryEfficiency * Time.deltaTime;
            if (currentArmor > maxArmor)
                currentArmor = maxArmor;
        }
    }
    public void InitiateParameter(int initmaxArmor, int initmaxEnergy)
    {
        maxArmor = initmaxArmor;
        currentEnergy = initmaxEnergy;
        currentArmor = maxArmor;
        currentEnergy = maxEnergy;
    }

    public bool ConsumeEnergy(int amountEnergyComsumned)
    {
        if (amountEnergyComsumned > currentEnergy) return false; //insufficient energy 
        currentEnergy -= amountEnergyComsumned;
        lastEnergyUsedTime = Time.time;
        return true;
    }
    
    public void TakeDamage(int damage)
    {
        currentArmor -= damage;
        lastDamageTime = Time.time;
        
        //


        if (currentArmor <= 0)
        {
            //death
        }
    }
}
