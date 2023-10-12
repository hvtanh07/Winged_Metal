using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class ShootTarget : ActionNode
{
    public float energyLevelThreshold;
    public int amountOfEnergyToRestore;
    bool shouldFiring;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.currentEn < energyLevelThreshold)
        {
            blackboard.RegeningEn = true;
        }
        else if (blackboard.currentEn >= amountOfEnergyToRestore)
        {
            blackboard.RegeningEn = false;
        }

        blackboard.ai.Attack(blackboard.playerPos.position - context.transform.position, !blackboard.RegeningEn);
        return State.Success;
    }
}
