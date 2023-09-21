using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckHaveLastSeen : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        if(blackboard.haveLastSeenPos){
            var state = child.Update();
            return state;
        }
        return State.Failure;
    }
}
