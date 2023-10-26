using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class AI : VehicleSystem
{

    protected Vector2 targetLastPos;
    protected NavMeshPath path;
    protected int index;
    public BehaviourTreeRunner behaviour;
    public Collider2D patrolArea;

    protected void InitParameters() //called in Start() of child class
    {
        path = new NavMeshPath();
        behaviour = GetComponent<BehaviourTreeRunner>();
        behaviour.tree.blackboard.randomArea = patrolArea;
    }
    public void OnEnable()
    {
        vehicle.ID.events.OnBeingHit += OnBeingHit;
        vehicle.ID.events.OnTargetDetected += OnhaveTargets;
    }

    public void OnhaveTargets(List<Transform> targetsList){
        behaviour.tree.blackboard.targetList = targetsList;
    }
    
    public void Dash(Vector2 dashDirection = default)
    {
        vehicle.ID.events.OnDashCalled?.Invoke(dashDirection);
    }

    public void OnBeingHit(Vector2 shootPoint)
    {
        behaviour.tree.blackboard.beingHit = true;
        behaviour.tree.blackboard.lastSeenPosition = shootPoint;
    }

    public void SecondAttackCall(List<Transform> targets)
    {
        Transform[] target = behaviour.tree.blackboard.targetList.ToArray();
        vehicle.ID.events.On2ndAttackCalled?.Invoke(target);
    }


    public void Attack(Vector2 targetDirection, bool openFire)
    {
        vehicle.ID.events.OnAttackDirectionChange?.Invoke(targetDirection, openFire);
    }

    public void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, behaviour.tree.blackboard.movementTarget, NavMesh.AllAreas, path);
        targetLastPos = behaviour.tree.blackboard.movementTarget;
        index = 0;
    }

}
