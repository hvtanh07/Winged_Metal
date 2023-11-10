using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingExplosive : VehicleSecondAttack
{
    public Transform missileShootingPoint;
    public int spawnTime = 3;
    int currentCall = 1;
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
        print("he");
        if (Time.time - lastAttackTime < cooldown) return;
        if (!ableToShoot) return;
        StartCoroutine(SpawnSequence(target[0]));
        lastAttackTime = Time.time;
    }
    IEnumerator SpawnSequence(Transform target){
        
        StartSpawnSequence(target);
        yield return new WaitForSeconds(0.3f);
        currentCall++;
        if(currentCall >spawnTime){
            currentCall = 0;
            lastAttackTime = Time.time;
        }else{
            currentCall++;
            StartCoroutine(SpawnSequence(target));
        }
    }
    void StartSpawnSequence(Transform target)
    {
        GameObject missile = ObjectPooler.SharedInstance.GetPooledObject("TrackingExplosive");
        if (missile != null)
        {
            vehicle.events.OnEnUsed?.Invoke(enConsum);
            missile.transform.position = missileShootingPoint.transform.position;
            missile.transform.rotation = missileShootingPoint.transform.rotation;
            missile.SetActive(true);
            missile.GetComponent<BulletScript>().SetParameter(damage, vehicle.side);
            missile.GetComponent<DelayedTrackingExplosive>().AssignTarget(target.position);

        }
        lastAttackTime = Time.time;
    }
}
