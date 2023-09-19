using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckPlayerDetected : DecoratorNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if ((blackboard.playerPos.position - context.transform.position).magnitude <= blackboard.detectingDistance &&
            !Physics2D.Linecast(blackboard.playerPos.position,context.transform.position, blackboard.viewBlock))
        {
            //blackboard.ChasingTarget = true;
            var state = child.Update();
            return state;
        }
        return State.Failure;
    }
}

