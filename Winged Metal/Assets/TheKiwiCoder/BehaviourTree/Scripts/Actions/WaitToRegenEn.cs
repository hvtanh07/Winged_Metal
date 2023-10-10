using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class WaitToRegenEn : ActionNode
{
    public float amountToRegen;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.currentEn < amountToRegen){
            return State.Running;
        }
        return State.Success;
    }
}
