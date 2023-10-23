using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Alerted : DecoratorNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.targets.Count > 0)
        {
            //blackboard.target = blackboard.targets[0].position;
            blackboard.lastSeenPosition = blackboard.target;
            blackboard.target = context.transform.position;
            if (!blackboard.haveLastSeenPos) blackboard.haveLastSeenPos = true;
            var state = child.Update();
            return state;
        }
        else if (blackboard.beingHit){
            blackboard.target = blackboard.lastSeenPosition;
        }
        return State.Failure;
    }
}
