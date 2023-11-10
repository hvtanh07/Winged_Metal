using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAutoGun : VehicleSystem
{
    [Header("Autogun movement")]
    public float rotatingSpeed;
    public float RadarRange;
    public LayerMask scanTarget;
    public LayerMask obstacle;
    [Space]
    [Header("Autogun attack")]
    public Transform shootingPoint;
    public int damage;
    public float fireRate;
    private float lastShotTime;
    Collider2D[] targetInRadius;
    private Quaternion toRotation;
    public List<Transform> visibleTargets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindVisibleTarget();
        RemoveTargetFromList();
        if (visibleTargets.Count > 0)
        {
            Vector2 direction = visibleTargets[0].position - transform.position;
            toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotatingSpeed * Time.fixedDeltaTime);
            if (toRotation == transform.rotation && Time.time - lastShotTime >= 1 / fireRate) //if canon is allign with shooting direction.
            {
                Shoot();
            }
        }
    }
    public void FindVisibleTarget()
    {
        targetInRadius = Physics2D.OverlapCircleAll(transform.position, RadarRange, scanTarget);

        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform target = targetInRadius[i].transform;
            Vector2 dirTarget = target.position - transform.position;
            if (Physics2D.Linecast(transform.position, target.position, obstacle)) continue; //if target are blocked by obstacle then skip it
            if (!visibleTargets.Contains(target))
            {
                visibleTargets.Add(target);
                vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
            }
        }
    }
    public void RemoveTargetFromList()
    {
        if (visibleTargets.Count == 0) return;

        for (int i = 0; i < visibleTargets.Count; i++)
        {
            Transform target = visibleTargets[i];

            //Null Check---------------------------------------------
            if (target == null)
            {
                visibleTargets.RemoveAt(i);
                vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
            }

            //not active in hierarchy
            if (!target.gameObject.activeInHierarchy)
            {
                visibleTargets.Remove(target);
                vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
                continue;
            }

            //Out of View Radius---------------------------------------------
            if (Vector3.Distance(transform.position, target.position) > RadarRange)
            {
                visibleTargets.Remove(target);
                vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
                continue;
            }

            //Have obstacle blocking view-------------------------------------
            if (Physics2D.Linecast(transform.position, target.position, obstacle))
            {
                visibleTargets.Remove(target);
                vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
                continue;
            }
        }

    }


    public void Shoot()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            //vehicle.ID.events.OnEnUsed?.Invoke(enConsum);
            bullet.transform.position = shootingPoint.transform.position;
            bullet.transform.rotation = shootingPoint.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<BulletScript>().SetParameter(damage, vehicle.side, shootingPoint.transform.position);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 35f;
        }
        lastShotTime = Time.time;
    }
}
