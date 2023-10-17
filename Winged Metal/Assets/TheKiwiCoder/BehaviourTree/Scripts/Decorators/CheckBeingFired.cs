using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckBeingFired : DecoratorNode
{
    public float projectileRadarRadius;
    public LayerMask projectile;
    public VehicleSide shooterToFind;
    [Range(0.0f, 100.0f)]
    public int radarEfficency;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(context.transform.position, projectileRadarRadius, projectile);
        if (hit == null) return State.Success; //not a bullet? do nothing

        if (hit.gameObject.GetComponent<BulletScript>().bulletOwner != shooterToFind) return State.Success; //doesn't have bullet script with the shooter on the same side? do nothing

        if (Random.Range(0, 101) > radarEfficency) return State.Success;

        blackboard.bulletPos = hit.gameObject.transform.position;
        var state = child.Update();
        return state;
    }
}
