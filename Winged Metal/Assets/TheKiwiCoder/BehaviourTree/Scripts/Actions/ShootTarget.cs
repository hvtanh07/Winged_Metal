using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ShootTarget : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.ai.GiveAttackDirection(blackboard.playerPos.position - context.transform.position);
        return State.Success;
    }
}
