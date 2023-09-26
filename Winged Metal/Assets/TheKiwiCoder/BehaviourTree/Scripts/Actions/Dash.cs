using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TheKiwiCoder;

public class Dash : ActionNode
{
    public UnityEvent DashEvent;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector2 dashDirection = Vector2.Perpendicular((Vector2)context.gameObject.transform.position - blackboard.bulletPos);
        context.gameObject.GetComponent<EnemyAI>().Dash(dashDirection);
        return State.Success;
    }
}
