using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class UpdateTargetPosition : ActionNode
{
    protected override void OnStart() {
        blackboard.target = blackboard.playerPos.position;
    }

    protected override void OnStop() {
        
    }

    protected override State OnUpdate() {
        if(blackboard.target == null) return State.Failure;
        return State.Success;
    }
}
