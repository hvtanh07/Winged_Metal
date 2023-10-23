using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class UpdateLastSeenPos : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //blackboard.lastSeenPosition = blackboard.playerPos.position;
        blackboard.haveLastSeenPos = true;
        blackboard.beingHit = false;
        return State.Success;
    }
}
