using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TheKiwiCoder;

public class Dash : ActionNode
{
    Vector2 bulletDirection;
    protected override void OnStart() {
        bulletDirection = (Vector2)context.gameObject.transform.position - blackboard.bulletPos;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector2 dashDirection = Quaternion.Euler(0f,0f, Random.Range(20,340)) * bulletDirection;
        blackboard.ai.Dash(dashDirection);
        return State.Success;
    }
}
