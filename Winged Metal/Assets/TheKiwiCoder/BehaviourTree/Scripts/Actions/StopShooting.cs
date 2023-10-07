using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class StopShooting : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //blackboard.attack.direction = Vector2.zero;
        blackboard.ai.GiveAttackDirection(Vector2.zero);
        return State.Success;
    }
}