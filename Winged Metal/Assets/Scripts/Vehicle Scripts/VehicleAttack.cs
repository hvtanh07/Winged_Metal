using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAttack : VehicleSystem
{
    public enum BulletOwner
    {
        player,
        enemy,
        ally
    }
    [HideInInspector]
    [Header("Cannon Stats--------")]
    public Vector2 direction;

    public BulletOwner bulletOwner;

    public float rotatingSpeed;
    [Header("Main Cannon--------")]
    public Transform shootingPoint;
    public int damage;
    public float fireRate;
    public int enConsum;
    [Header("2nd Weapon--------")]//will move this to seperate script

    public Transform[] missileShootingPoint;
    public int secondEnConsum;
    public float secondCooldown;

    private Quaternion toRotation;

    private float lastShotTime;
    private float last2ShotTime;
    // Start is called before the first frame update
    
    void OnEnable(){
        vehicle.ID.events.CallToShoot += Shoot;
    }
    
    private void FixedUpdate()
    {
        if (direction != Vector2.zero) // check moving and rotate to the moving direction
        {
            toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotatingSpeed * Time.fixedDeltaTime);
            if (toRotation == transform.rotation && Time.time - lastShotTime >= 1 / fireRate) //if canon is allign with shooting direction.
            {
                InitShoot();
            }
        }
    }
    public void InitShoot(){
        vehicle.ID.events.GetEntoShoot?.Invoke(enConsum);
    }
    public void Shoot()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = shootingPoint.transform.position;
            bullet.transform.rotation = shootingPoint.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<BulletScript>().SetParameter(damage, bulletOwner);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 35f;
        }
        lastShotTime = Time.time;
    }
    public void ShootMissile(Transform target)
    {
        if (Time.time - last2ShotTime < secondCooldown) return;

        foreach (Transform Point in missileShootingPoint)
        {
            GameObject missile = ObjectPooler.SharedInstance.GetPooledObject("Missile");
            if (missile != null)
            {
                //if (resources.ConsumeEnergy(enConsum))
                {
                    missile.transform.position = Point.transform.position;
                    missile.transform.rotation = Point.transform.rotation;
                    missile.SetActive(true);
                    missile.GetComponent<BulletScript>().SetParameter(damage, bulletOwner);
                    missile.GetComponent<MissileScript>().AssignTarget(target);
                }
            }
            last2ShotTime = Time.time;
        }

    }
}
