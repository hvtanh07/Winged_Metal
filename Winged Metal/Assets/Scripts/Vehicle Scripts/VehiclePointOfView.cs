using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class VehiclePointOfView : VehicleSystem
{
    public float viewRadius;
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
        targetInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, scanTarget);

        //visibleTargets.Clear();

        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform target = targetInRadius[i].transform;
            Vector2 dirTarget = target.position - transform.position;
            if (Vector2.Angle(dirTarget, transform.up) >= viewAngle / 2) continue; //if target are not within view range then skip it

            if (!Physics2D.Linecast(transform.position, target.position, obstacle))
            {
                if (!visibleTargets.Contains(target))
                {
                    visibleTargets.Add(target);
                    Debug.Log("Run once");
                    vehicle.ID.events.OnTargetDetected?.Invoke(visibleTargets);
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
                Debug.Log("Removed by target null");
                visibleTargets.RemoveAt(i);
            }

            //not active in hierarchy
            if (!target.gameObject.activeInHierarchy)
            {
                Debug.Log("Removed by not active");
                visibleTargets.Remove(target);
                continue;
            }

            //Out of View Radius---------------------------------------------
            if (Vector3.Distance(transform.position, target.position) > viewRadius || Physics2D.Linecast(transform.position, target.position, obstacle))
            {
                Debug.Log("Removed by not in view radius");
                visibleTargets.Remove(target);
                continue;
            }


            //Out of FOV---------------------------------------------
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.up, dirToTarget) > viewAngle / 2)
            {
                Debug.Log("Removed by out of FOV");
                visibleTargets.Remove(target);
            }


        }

    }
}
