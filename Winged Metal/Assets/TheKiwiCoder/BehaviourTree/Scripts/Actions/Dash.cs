using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TheKiwiCoder;

public class Dash : ActionNode
{
    Vector2 bulletDirection;
    protected override void OnStart()
    {
        bulletDirection = (Vector2)context.gameObject.transform.position - blackboard.bulletPos;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 dashDirection = new Vector2();
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
            dashDirection = Quaternion.Euler(0f, 0f, 90) * bulletDirection;
        else
            dashDirection = Quaternion.Euler(0f, 0f, -90) * bulletDirection;

        if (Physics2D.Raycast(context.transform.position, dashDirection,10)) //if the selected direction hit something then dash the other direction
            dashDirection = Quaternion.Euler(0f, 0f, 180) * dashDirection;
            
        blackboard.ai.Dash(dashDirection);
        return State.Success;
    }
}
