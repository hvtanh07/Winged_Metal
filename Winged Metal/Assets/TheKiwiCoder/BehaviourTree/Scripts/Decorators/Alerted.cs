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
        if (blackboard.targetList.Count > 0)
        {
            blackboard.lastSeenPosition = blackboard.targetList[0].position;
            blackboard.movementTarget = context.transform.position;
            if (!blackboard.haveLastSeenPos) blackboard.haveLastSeenPos = true;
            var state = child.Update();
            return state;
        }
        else if (blackboard.beingHit){
            if (!blackboard.haveLastSeenPos) blackboard.haveLastSeenPos = true;
            blackboard.movementTarget = blackboard.lastSeenPosition;
            blackboard.beingHit = false;
        }
        return State.Failure;
    }
}
