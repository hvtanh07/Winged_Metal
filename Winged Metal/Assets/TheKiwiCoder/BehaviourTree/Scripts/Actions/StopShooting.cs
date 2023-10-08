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
        blackboard.ai.Attack(Vector2.zero);
        return State.Success;
    }
}