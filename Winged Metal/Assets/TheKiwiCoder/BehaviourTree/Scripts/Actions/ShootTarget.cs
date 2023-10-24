using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class ShootTarget : ActionNode
{
    public float attackDuration;
    public float cooldownDuration;
    float lastSwitch;
    bool shooting;
    protected override void OnStart()
    {
        shooting = true;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //if (shooting && Time.time - lastSwitch > attackDuration){
        //    shooting = false;
        //    lastSwitch = Time.time;
        //}else if (!shooting && Time.time - lastSwitch > cooldownDuration){
        //    shooting = true;
        //    lastSwitch = Time.time;
        //}

        blackboard.ai.Attack(blackboard.targetList[0].position - context.transform.position, shooting);
        return State.Success;
    }
}
