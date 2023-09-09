using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIParent : MonoBehaviour
{
    public float targetOffsetRecalculate;
    public Transform target;
    protected Vector3 targetLastPos;
    protected NavMeshPath path;
    protected int index;
    // Start is called before the first frame update
    protected void InitParameters(){ //called in Start() of child class
        path = new NavMeshPath();
    }


    // Update is called once per frame
    void Update()
    {

    }
    protected void RecalculatePath()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        targetLastPos = target.position;
        index = 0;
    }
    
}
