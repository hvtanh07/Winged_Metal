using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckRemainEn : DecoratorNode
{
    public float energyLevelThreshold;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.currentEn >= energyLevelThreshold)
        {
            var state = child.Update();
            return state;
        }
        else
            return State.Success;
    }
}
