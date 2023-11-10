using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleArmor : VehicleSystem
{
    //Armor
    public int tankMaxArmor;
    public int enShield;
    public float ehp = 0.01f;
    [SerializeField]
    protected float tankCurrentArmor;
    protected float lastDamageTime;
    public VehicleSide[] damageDealers;
    public int recoveryEfficiency;
    float damageDeduction;


     protected void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        float damageDeduction = (enShield*ehp)/(1+enShield*ehp);
        tankCurrentArmor = vehicle.vehicleArmor;
        vehicle.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
        switch (vehicle.side)
        {
            case VehicleSide.player:
            case VehicleSide.ally:
                {
                    damageDealers = new VehicleSide[] { VehicleSide.enemy };
                    break;
                }
            case VehicleSide.enemy:
                {
                    damageDealers = new VehicleSide[] { VehicleSide.ally, VehicleSide.player };
                    break;
                }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BulletScript bullet = col.gameObject.GetComponent<BulletScript>();
        if (bullet == null) return; //if object is not bullet then no need to do anything
        foreach (VehicleSide damageDealer in damageDealers)
        {
            if (damageDealer == bullet.bulletOwner)
            {
                TakeDamage(bullet.GetDamage(), bullet.shootPoint);
                col.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void Update()
    {
        if (tankCurrentArmor < tankMaxArmor && Time.time - lastDamageTime > 5.0f)
        {
            tankCurrentArmor += recoveryEfficiency * Time.deltaTime;
            if (tankCurrentArmor > tankMaxArmor)
                tankCurrentArmor = tankMaxArmor;
            vehicle.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
        }
    }
    public void InitiateParameter(int maxArmor, int Shield)
    {
        enShield = Shield;
        tankMaxArmor = maxArmor;
        vehicle.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
    }

    public void TakeDamage(int damage, Vector3 shootpoint)
    {
        int finalDamage = Mathf.RoundToInt(damage*(1-damageDeduction));
        tankCurrentArmor -= finalDamage;
        vehicle.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
        vehicle.events.OnBeingHit?.Invoke(shootpoint);
        lastDamageTime = Time.time;

        if (tankCurrentArmor <= 0)
        {
            vehicle.events.OnDeath?.Invoke();
        }
    }
}
