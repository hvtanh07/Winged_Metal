using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Dash : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector2 dashDirection = Vector2.Perpendicular(blackboard.bulletPos);
        context.gameObject.GetComponent<EnemyAI>().Dash(dashDirection);
        return State.Success;
    }
}
