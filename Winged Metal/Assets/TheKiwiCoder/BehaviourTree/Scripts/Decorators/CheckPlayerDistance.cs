using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckPlayerDistance : DecoratorNode
{
    public float detectingDistance;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if ((blackboard.playerPos.position - context.transform.position).magnitude <= detectingDistance &&
            !Physics2D.Linecast(blackboard.playerPos.position,context.transform.position, blackboard.viewBlock))
        {
            var state = child.Update();
            return state;
        }
        return State.Failure;
    }
}

