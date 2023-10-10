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
    public float mindashCoolDown;
    public float maxdashCoolDown;
    float dashCoolDown;
    float lastDashedTime;
    // Start is called before the first frame update
    protected void InitParameters()
    { //called in Start() of child class
        path = new NavMeshPath();
        behaviour = GetComponent<BehaviourTreeRunner>();
        behaviour.tree.blackboard.randomArea = patrolArea;
        dashCoolDown = Random.Range(mindashCoolDown, maxdashCoolDown);
    }
    private void OnEnable()
    {
        vehicle.ID.events.OnEnUpdate += updateBTEn;
        vehicle.ID.events.OnBeingHit += OnBeingHit;
    }

    public void Dash(Vector2 dashDirection = default)
    {
        if (Time.time - lastDashedTime >= dashCoolDown)
        {
            vehicle.ID.events.OnDashCalled?.Invoke(dashDirection);
            lastDashedTime = Time.time;
            dashCoolDown = Random.Range(mindashCoolDown, maxdashCoolDown);
        }
    }

    public void updateBTEn(float currentEn)
    {
        behaviour.tree.blackboard.currentEn = currentEn;
    }

    public void OnBeingHit()
    {
        behaviour.tree.blackboard.beingHit = true;
    }

    public void SecondAttackCall()
    {
        vehicle.ID.events.On2ndAttackCalled(behaviour.tree.blackboard.playerPos);
    }


    public void Attack(Vector2 targetDirection, bool openFire)
    {
        Debug.Log("fire");
        vehicle.ID.events.OnAttackDirectionChange?.Invoke(targetDirection, openFire);
    }

    public void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, behaviour.tree.blackboard.target, NavMesh.AllAreas, path);
        targetLastPos = behaviour.tree.blackboard.target;
        index = 0;
    }

}
