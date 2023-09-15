using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandomPosition : ActionNode
{

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float x = 0;
        float y = 0;
        do {
            x = Random.Range(blackboard.randomArea.bounds.min.x, blackboard.randomArea.bounds.max.x);
            y = Random.Range(blackboard.randomArea.bounds.min.y, blackboard.randomArea.bounds.max.y);
        } while (blackboard.randomArea.OverlapPoint(new Vector2(x, y)));
        Debug.Log("Run");
        blackboard.target = new Vector2(x,y);
        //check player reach target
        return State.Success;
    }
}
