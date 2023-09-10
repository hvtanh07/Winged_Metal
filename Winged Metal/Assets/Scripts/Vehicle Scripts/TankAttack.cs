using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [HideInInspector]
    public Vector2 direction;
    public float rotatingSpeed;
    private Quaternion toRotation;
    private TankResources resources;
    public float fireRate;
    private float lastShotTime;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (direction != Vector2.zero) // check moving and rotate to the moving direction
        {
            toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotatingSpeed * Time.fixedDeltaTime);
            if (toRotation == transform.rotation && Time.time - lastShotTime >= 1/fireRate) //if canon is allign with shooting direction.
            {
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        lastShotTime = Time.time;
        print("shoot");
    }
}
