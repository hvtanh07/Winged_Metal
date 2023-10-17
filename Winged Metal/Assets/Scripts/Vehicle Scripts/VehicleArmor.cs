using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleArmor : VehicleSystem
{
    //Armor
    public int tankMaxArmor;
    [SerializeField]
    protected float tankCurrentArmor;
    protected float lastDamageTime;
    public VehicleSide[] damageDealers;
    public int recoveryEfficiency;


     protected void Start() //REMOVE WHEN THERE's SCRIPT TO READ FROM SCRIPTABLE OBJECT
    {
        tankCurrentArmor = tankMaxArmor;
        vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
    }
    void OnEnable()
    {
        vehicle.ID.events.OnTakeHit += TakeDamage;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        BulletScript bullet = col.gameObject.GetComponent<BulletScript>();
        if (bullet == null) return; //if object is not bullet then no need to do anything
        foreach (VehicleSide damageDealer in damageDealers)
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
        if (tankCurrentArmor < tankMaxArmor && Time.time - lastDamageTime > 5.0f)
        {
            tankCurrentArmor += recoveryEfficiency * Time.deltaTime;
            if (tankCurrentArmor > tankMaxArmor)
                tankCurrentArmor = tankMaxArmor;
            vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
        }
    }
    public void InitiateParameter(int maxArmor)
    {
        tankMaxArmor = maxArmor;
        vehicle.ID.events.OnArmorUpdate?.Invoke(tankCurrentArmor);
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
    public float GetCurrentArmor()
    {
        return tankCurrentArmor;
    }
}
