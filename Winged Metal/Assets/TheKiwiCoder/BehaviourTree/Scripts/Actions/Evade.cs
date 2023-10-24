using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Evade : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector2 moveDirection = Vector2.Perpendicular((Vector2)context.transform.position - blackboard.bulletPos);
        //blackboard.target = (Vector2)context.transform.position + moveDirection;
        
        return State.Success;
    }
}
