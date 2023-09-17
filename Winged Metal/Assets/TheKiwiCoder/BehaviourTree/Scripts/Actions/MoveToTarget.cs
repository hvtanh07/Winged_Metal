using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToTarget : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (((Vector2)context.transform.position - blackboard.target).magnitude <= 1f)
            return State.Success;
        else
            return State.Running;
    }
}
