using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class VehicleAttack : VehicleSystem
{

    [HideInInspector]
    [Header("Cannon Stats--------")]
    public Vector2 direction;

    public VehicleSide bulletOwner;

    public float rotatingSpeed;
    [Header("Main Cannon--------")]
    public Transform shootingPoint;
    public int damage;
    public float fireRate;
    public int enConsum;
    [SerializeField]
    bool ableToShoot;
    bool openFire;
    private Quaternion toRotation;
    private float lastShotTime;


    void OnEnable()
    {
        vehicle.ID.events.OnAttackDirectionChange += ChangeDirection;
        vehicle.ID.events.OnEnUpdate += UpdateAmountEn;
        ableToShoot = true;
    }

    private void ChangeDirection(Vector2 newdirection, bool isOpenFire)
    {
        direction = newdirection;
        openFire = isOpenFire;
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
            if (toRotation == transform.rotation && Time.time - lastShotTime >= 1 / fireRate && openFire) //if canon is allign with shooting direction.
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
                bullet.GetComponent<BulletScript>().SetParameter(damage, bulletOwner, shootingPoint.transform.position);
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 35f;
            }
            lastShotTime = Time.time;
        }
    }

}
