using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToRandomPosition : ActionNode
{
    protected override void OnStart()
    {
        blackboard.movementTarget = blackboard.randomPosition;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.ai.Attack(blackboard.movementTarget - (Vector2)context.transform.position, false);
        if (((Vector2)context.transform.position - blackboard.movementTarget).magnitude <= 1f)
            return State.Success;
        else
            return State.Running;
    }
}
