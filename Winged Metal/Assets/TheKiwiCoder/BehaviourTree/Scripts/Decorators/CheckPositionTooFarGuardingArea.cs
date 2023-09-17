using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckPositionTooFarGuardingArea : DecoratorNode
{
    public float checkDistance;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if ((blackboard.randomPosition - (Vector2)context.transform.position).magnitude >= checkDistance)
        {
            var state = child.Update();
            return state;
        }
        return State.Success;
    }
}
