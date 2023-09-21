using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    public enum BulletOwner{
        player,
        enemy,
        ally
    }
    [HideInInspector]
    public Vector2 direction;
    public int enConsum;
    public BulletOwner bulletOwner;
    public int damage;
    public Transform shootingPoint;
    public float rotatingSpeed;
    private Quaternion toRotation;
    private TankResources resources;
    public float fireRate;
    private float lastShotTime;
    // Start is called before the first frame update
    void Start(){
        resources = GetComponentInParent<TankResources>();
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
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            resources.ConsumeEnergy(enConsum);
            bullet.transform.position = shootingPoint.transform.position;
            bullet.transform.rotation = shootingPoint.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<BulletScript>().SetParameter(damage, bulletOwner);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 35f;
        }
        lastShotTime = Time.time;
        //print("shoot");
    }
}
