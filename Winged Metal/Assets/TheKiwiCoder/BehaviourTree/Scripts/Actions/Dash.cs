using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TheKiwiCoder;

public class Dash : ActionNode
{
    Vector2 bulletDirection;
    protected override void OnStart() {
        bulletDirection = (Vector2)context.gameObject.transform.position - blackboard.bulletPos;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector2 dashDirection = Quaternion.Euler(0f,0f, Random.Range(20,340)) * bulletDirection;
        
        //Vector2 dashDirection = Vector2.Perpendicular((Vector2)context.gameObject.transform.position - blackboard.bulletPos);
        blackboard.movement.Dash(dashDirection);
        return State.Success;
    }
}
