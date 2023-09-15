using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class GetPlayerPosition : ActionNode
{
    protected override void OnStart() {
        blackboard.playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(blackboard.playerPos == null) return State.Failure;
        return State.Success;
    }
}
