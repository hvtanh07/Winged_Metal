using System.Collections;
using System.Collections.Generic;
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
    }

    public void FindVisibleTarget()
    {
        targetInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, scanTarget);

        visibleTargets.Clear();

        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform target = targetInRadius[i].transform;
            Vector2 dirTarget = target.position - transform.position;
            if (Vector2.Angle(dirTarget, transform.up) >= viewAngle / 2) continue; //if target are not within view range then skip it

            if (!Physics2D.Linecast(transform.position, target.position, obstacle))
            {
                visibleTargets.Add(target);
                vehicle.ID.events.OnTargetDetected?.Invoke(visibleTargets);
            }
        }
    }
}
