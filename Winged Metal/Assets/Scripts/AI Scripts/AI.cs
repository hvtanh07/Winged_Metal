using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class AI : VehicleSystem
{
    [HideInInspector]
    public Vector2 target;
    protected Vector2 targetLastPos;
    protected NavMeshPath path;
    protected int index;
    public BehaviourTreeRunner BT;


    protected void InitParameters()
    { //called in Start() of child class
        path = new NavMeshPath();
        BT = GetComponent<BehaviourTreeRunner>();
        BT.tree.blackboard.ai = this;
    }
    public void GiveAttackDirection(Vector2 direction)
    {
        vehicle.ID.events.OnAttackDirectionChange?.Invoke(direction);
    }
    public void Dash(Vector2 direction = default)
    {
        vehicle.ID.events.OnDash?.Invoke(direction);
    }

    public void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, BT.tree.blackboard.target, NavMesh.AllAreas, path);
        targetLastPos = BT.tree.blackboard.target;
        index = 0;
    }
    public float GetCurrentEn()
    {
        return vehicle.currentEnergy;
    }
}
