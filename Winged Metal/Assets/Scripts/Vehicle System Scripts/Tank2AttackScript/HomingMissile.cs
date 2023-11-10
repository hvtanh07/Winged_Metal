using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : VehicleSecondAttack
{
    public Transform[] missileShootingPoint;
    void Start()
    {
        ableToShoot = true;
    }
    void OnEnable()
    {
        vehicle.events.On2ndAttackCalled += Attack;
        vehicle.events.OnEnUpdate += UpdateAmountEn;
    }

    public override void Attack(Transform[] target)
    {
        if (Time.time - lastAttackTime < cooldown) return;
        if (!ableToShoot) return;
        int i = 0;
        foreach (Transform Point in missileShootingPoint)
        {
            GameObject missile = ObjectPooler.SharedInstance.GetPooledObject("Missile");
            if (missile != null)
            {
                vehicle.events.OnEnUsed?.Invoke(enConsum);
                missile.transform.position = Point.transform.position;
                missile.transform.rotation = Point.transform.rotation;
                missile.SetActive(true);
                missile.GetComponent<BulletScript>().SetParameter(damage, vehicle.side);
                missile.GetComponent<MissileScript>().AssignTarget(target[i]);
                i++;
                if (i > target.Length - 1)
                    i = 0;
            }
        }
        lastAttackTime = Time.time;
    }
}
