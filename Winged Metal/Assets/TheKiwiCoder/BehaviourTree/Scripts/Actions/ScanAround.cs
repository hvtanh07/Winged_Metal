using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ScanAround : ActionNode
{

    public float duration = 1;
    float startTime;
    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            blackboard.haveLastSeenPos = false;
            return State.Success;
        }
        return State.Running;
    }
}
