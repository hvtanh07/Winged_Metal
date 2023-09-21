using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckRemainEn : DecoratorNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.currentEn >= 300)
            return State.Success;
        else
            return State.Failure;
    }
}
