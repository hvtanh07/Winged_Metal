using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToRandomPosition : ActionNode
{
    protected override void OnStart()
    {
        blackboard.target = blackboard.randomPosition;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (((Vector2)context.transform.position - blackboard.randomPosition).magnitude <= 1f)
            return State.Success;
        else
            return State.Running;
    }
}
