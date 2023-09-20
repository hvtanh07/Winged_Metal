using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToLastSeen : ActionNode
{
    protected override void OnStart() {
        blackboard.target = blackboard.lastSeenPosition;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (((Vector2)context.transform.position - blackboard.lastSeenPosition).magnitude <= 1f){
            blackboard.haveLastSeenPos = false;
            //return State.Failure;
            return State.Success;
        }
            
        else
            return State.Running;
    }
}
