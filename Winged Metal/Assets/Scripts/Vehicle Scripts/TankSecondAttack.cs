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
    public int enConsum;
    public float cooldown;
  
    public virtual void Attack(){
        
    }
}
