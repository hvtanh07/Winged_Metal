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
        Vector2 RanPos;
        do {
            x = Random.Range(blackboard.randomArea.bounds.min.x + 2, blackboard.randomArea.bounds.max.x - 2);
            y = Random.Range(blackboard.randomArea.bounds.min.y + 2, blackboard.randomArea.bounds.max.y - 2);
            RanPos = new Vector2(x,y);
        } while (blackboard.randomArea.OverlapPoint(RanPos) && (RanPos - blackboard.randomPosition).magnitude < 3);
        blackboard.randomPosition = new Vector2(x,y);
        //check player reach target
        return State.Success;
    }
}
