using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class AIParent : MonoBehaviour
{
    public VehicleID ID;
    public float targetOffsetRecalculate;
    public Vector2 target;
    protected Vector2 targetLastPos;
    protected NavMeshPath path;
    protected int index;
    public BehaviourTreeRunner behaviour;
    public LayerMask bulletBlock;
    public Collider2D patrolArea;
    // Start is called before the first frame update
    protected void InitParameters(){ //called in Start() of child class
        path = new NavMeshPath();
        behaviour = GetComponent<BehaviourTreeRunner>();
        behaviour.tree.blackboard.randomArea = patrolArea;
    }


    public void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, behaviour.tree.blackboard.target, NavMesh.AllAreas, path);
        targetLastPos = behaviour.tree.blackboard.target;
        index = 0;
    }
    
}
