using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckBeingFired : DecoratorNode
{
    public float projectileRadarRadius;
    public LayerMask projectile;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(context.transform.position, projectileRadarRadius, projectile);
        if (hit == null) return State.Failure; 

        if (hit.gameObject.GetComponent<BulletScript>().bulletOwner == TankAttack.BulletOwner.player)
        {
            blackboard.bulletPos = hit.gameObject.transform.position;
            var state = child.Update();
            return state;
        }

        //return State.Failure;
    }
}
