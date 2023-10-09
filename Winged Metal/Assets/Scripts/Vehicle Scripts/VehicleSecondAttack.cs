using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleSecondAttack : VehicleSystem
{
    public enum WeaponType {
        HomingMissile,
        Blade,
        Mine
    }
    public WeaponType weaponType;
    public int damage;
    public int enConsum;
    public float cooldown;
    public VehicleAttack.BulletOwner bulletOwner;
    protected float lastAttackTime;
    protected VehicleAttack tankAttack;
    bool ableToShoot;
  
    public abstract void Attack(Transform target);
    protected void UpdateAmountEn(float currentEn)
    {
        ableToShoot = currentEn >= enConsum;
    }
}
