using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSecondAttack : MonoBehaviour
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
    protected float lastAttackTime;
    protected VehicleAttack tankAttack;
  
    public virtual void Attack(Transform target){
        
    }
}
