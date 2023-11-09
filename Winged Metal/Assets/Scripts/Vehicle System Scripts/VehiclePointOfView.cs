using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class VehiclePointOfView : VehicleSystem
{
    public float frontViewRadius;
    public float nearViewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask scanTarget;
    public LayerMask obstacle;


    Collider2D[] targetInRadius;
    public List<Transform> visibleTargets = new List<Transform>();

    void FixedUpdate()
    {
        FindVisibleTarget();
        RemoveTargetFromList();
    }

    public void FindVisibleTarget()
    {
        targetInRadius = Physics2D.OverlapCircleAll(transform.position, frontViewRadius, scanTarget);

        //visibleTargets.Clear();

        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform target = targetInRadius[i].transform;
            Vector2 dirTarget = target.position - transform.position;
            if (Physics2D.Linecast(transform.position, target.position, obstacle)) continue; //if target are not within view range then skip it

            if ((Vector2.Angle(dirTarget, transform.up) <= viewAngle / 2) || ((target.position - transform.position).magnitude < nearViewRadius))
            {
                if (!visibleTargets.Contains(target))
                {
                    visibleTargets.Add(target);
                    vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
                }
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
            if (Vector3.Distance(transform.position, target.position) > frontViewRadius)
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

            //Out of FOV---------------------------------------------
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if ((Vector3.Angle(transform.up, dirToTarget) > viewAngle / 2) && ((target.position - transform.position).magnitude > nearViewRadius))
            {
                visibleTargets.Remove(target);
                vehicle.events.OnTargetDetected?.Invoke(visibleTargets);
            }
        }

    }
    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, frontViewRadius);
        //Gizmos.DrawWireSphere(transform.position, nearViewRadius);
    }
}
