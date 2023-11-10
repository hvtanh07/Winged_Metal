using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleSecondAttack : VehicleSystem
{
    //public VehicleSide weaponType;
    public int damage;
    public int enConsum;
    public float cooldown;
    protected float lastAttackTime;
    protected bool ableToShoot;
  
    public abstract void Attack(Transform[] target);
    protected void UpdateAmountEn(float currentEn)
    {
        ableToShoot = currentEn >= enConsum;
    }
}
