using System.Collections;
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
    // Start is called before the first frame update
    protected void InitParameters()
    { //called in Start() of child class
        path = new NavMeshPath();
        behaviour = GetComponent<BehaviourTreeRunner>();
        behaviour.tree.blackboard.randomArea = patrolArea;
    }
    private void OnEnable() {
        vehicle.ID.events.OnEnUpdate += updateBTEn;
        vehicle.ID.events.OnBeingHit += OnBeingHit;
    }

    public void Dash(Vector2 dashDirection = default)
    {
        vehicle.ID.events.OnDashCalled?.Invoke(dashDirection);
    }

    public void updateBTEn(float currentEn){
        behaviour.tree.blackboard.currentEn = currentEn;
    }

    public void OnBeingHit(){
        behaviour.tree.blackboard.beingHit = true;
    }

    public void SecondAttackCall(){
        vehicle.ID.events.On2ndAttackCalled(behaviour.tree.blackboard.playerPos);
    }


    public void Attack(Vector2 targetDirection)
    {
        vehicle.ID.events.OnAttackDirectionChange?.Invoke(targetDirection);
    }

    public void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, behaviour.tree.blackboard.target, NavMesh.AllAreas, path);
        targetLastPos = behaviour.tree.blackboard.target;
        index = 0;
    }

}
