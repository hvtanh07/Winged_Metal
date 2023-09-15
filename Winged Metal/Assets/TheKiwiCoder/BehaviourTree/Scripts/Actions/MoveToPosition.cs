using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPosition : ActionNode
{
    protected override void OnStart() {
       
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //blackboard.target = blackboard.randomPosition;

        return State.Running;
    }
}
