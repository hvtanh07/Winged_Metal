using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : VehicleSecondAttack
{
    public Transform[] missileShootingPoint;
    void Start()
    {
        tankAttack = GetComponent<VehicleAttack>();
    }
    void OnEnable()
    {
        vehicle.ID.events.On2ndAttackCalled += Attack;
        vehicle.ID.events.OnEnUpdate += UpdateAmountEn;
    }

    public override void Attack(Transform target)
    {
        if (Time.time - lastAttackTime < cooldown) return;

        foreach (Transform Point in missileShootingPoint)
        {
            GameObject missile = ObjectPooler.SharedInstance.GetPooledObject("Missile");
            if (missile != null)
            {
                vehicle.ID.events.OnEnUsed?.Invoke(enConsum);
                missile.transform.position = Point.transform.position;
                missile.transform.rotation = Point.transform.rotation;
                missile.SetActive(true);
                missile.GetComponent<BulletScript>().SetParameter(damage, bulletOwner);
                missile.GetComponent<MissileScript>().AssignTarget(target);
            }
            lastAttackTime = Time.time;
        }
    }
}
