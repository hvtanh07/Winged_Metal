using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : TankSecondAttack
{
    public Transform[] missileShootingPoint;
    void Start(){
        tankAttack = GetComponent<TankAttack>();
    }
    public override void Attack(Transform target){
        if (Time.time - lastAttackTime < cooldown) return;

        foreach (Transform Point in missileShootingPoint)
        {
            GameObject missile = ObjectPooler.SharedInstance.GetPooledObject("Missile");
            if (missile != null)
            {
                //if (tankAttack.resources.ConsumeEnergy(enConsum))
                //{
                //    missile.transform.position = Point.transform.position;
                //    missile.transform.rotation = Point.transform.rotation;
                //    missile.SetActive(true);
                //    missile.GetComponent<BulletScript>().SetParameter(damage, bulletOwner);
                //    missile.GetComponent<MissileScript>().AssignTarget(target);
                //}
            }
            lastAttackTime = Time.time;
        }
    }
}
