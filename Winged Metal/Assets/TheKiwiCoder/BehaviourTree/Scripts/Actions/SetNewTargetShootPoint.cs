using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SetNewTargetShootPoint : ActionNode
{
    public float minimumShootingDistance;
    public float maxOffsetAngle;
    protected override void OnStart()
    {
        Vector2 newtarget;
        do
        {
        float offsetAngle = Random.Range(-maxOffsetAngle, maxOffsetAngle);
        Vector2 direction = (context.transform.position - blackboard.playerPos.position).normalized * Random.Range(minimumShootingDistance, blackboard.detectingDistance);
        Vector2 newdirection = Quaternion.Euler(0f, 0f, offsetAngle) * direction;

        newtarget = (Vector2)blackboard.playerPos.position + newdirection;
        }
        while (Physics2D.Linecast(blackboard.playerPos.position, newtarget, blackboard.viewBlock));

        if((newtarget - (Vector2)context.transform.position).magnitude < 2)
            newtarget = context.transform.position;
        blackboard.target = newtarget;
    }
    

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
