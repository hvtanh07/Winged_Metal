using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckBeingFired : DecoratorNode
{
    public float projectileRadarRadius;
    public LayerMask projectile;
    private Collider2D hit;
    protected override void OnStart() {
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(Physics2D.OverlapCircle(context.transform.position,projectileRadarRadius,projectile).gameObject.GetComponent<BulletScript>().bulletOwner == TankAttack.BulletOwner.player){
            var state = child.Update();
            return state;
        }

        return State.Success;
    }
}
