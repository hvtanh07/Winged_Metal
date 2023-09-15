using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class AIParent : MonoBehaviour
{
    public float targetOffsetRecalculate;
    public Transform target;
    protected Vector3 targetLastPos;
    protected NavMeshPath path;
    protected int index;
    public BehaviourTreeRunner behaviour;
    public LayerMask bulletBlock;
    // Start is called before the first frame update
    protected void InitParameters(){ //called in Start() of child class
        path = new NavMeshPath();
        behaviour = GetComponent<BehaviourTreeRunner>();
    }


    protected void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        targetLastPos = target.position;
        index = 0;
    }
    
}
