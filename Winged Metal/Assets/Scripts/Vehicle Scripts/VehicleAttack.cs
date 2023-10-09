using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
    bool ableToShoot;
    [Header("2nd Weapon--------")]//will move this to seperate script

    public Transform[] missileShootingPoint;
    public int secondEnConsum;
    public float secondCooldown;

    private Quaternion toRotation;

    private float lastShotTime;
    private float last2ShotTime;
    // Start is called before the first frame update

    void OnEnable()
    {
        vehicle.ID.events.OnAttackDirectionChange += ChangeDirection;
        vehicle.ID.events.OnEnUpdate += UpdateAmountEn;
        ableToShoot = true;
    }

    private void ChangeDirection(Vector2 newdirection)
    {
        direction = newdirection;
    }
    private void UpdateAmountEn(float currentEn)
    {
        ableToShoot = currentEn >= enConsum;
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero) // check moving and rotate to the moving direction
        {
            toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotatingSpeed * Time.fixedDeltaTime);
            if (toRotation == transform.rotation && Time.time - lastShotTime >= 1 / fireRate) //if canon is allign with shooting direction.
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        if (ableToShoot)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                vehicle.ID.events.OnEnUsed?.Invoke(enConsum);
                bullet.transform.position = shootingPoint.transform.position;
                bullet.transform.rotation = shootingPoint.transform.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<BulletScript>().SetParameter(damage, bulletOwner);
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 35f;
            }
            lastShotTime = Time.time;
        }
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
