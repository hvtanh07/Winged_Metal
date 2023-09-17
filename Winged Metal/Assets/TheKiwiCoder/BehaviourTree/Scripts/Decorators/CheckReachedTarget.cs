using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckReachedTarget : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if ((blackboard.target - (Vector2)context.transform.position).magnitude <= 1.0f)
        {
            var state = child.Update();
            return state;
        }
        return State.Success;
    }
}
