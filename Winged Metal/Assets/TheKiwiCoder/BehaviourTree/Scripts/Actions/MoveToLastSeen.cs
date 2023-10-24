using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToLastSeen : ActionNode
{
    
    protected override void OnStart()
    {
        blackboard.movementTarget = blackboard.lastSeenPosition;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.ai.Attack(blackboard.movementTarget - (Vector2)context.transform.position, false);
        Debug.Log(((Vector2)context.transform.position - blackboard.movementTarget).magnitude);
        if (((Vector2)context.transform.position - blackboard.movementTarget).magnitude <= 2f) // gotta check this again sometime, 
        {
            //blackboard.haveLastSeenPos = false;
            return State.Success;
        }
        else
            return State.Running;
    }
}
