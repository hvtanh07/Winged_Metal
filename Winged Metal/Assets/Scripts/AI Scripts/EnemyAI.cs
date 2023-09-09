using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : AIParent
{
    

    private TankMovement movementS;
    private TankAttack attackS;

    // Start is called before the first frame update
    void Awake()
    {
        movementS = GetComponent<TankMovement>();
        attackS = GetComponentInChildren<TankAttack>();
        targetLastPos = target.position;
    }
    private void Start()
    {
        InitParameters();
        //NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        RecalculatePath(); //calculate path for the first time.

        //ONLY USE WHEN SWITCHING TO NAVMESH AGENT COMPONENT
        //var agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    void Update()
    {
        if ((targetLastPos - target.position).magnitude > targetOffsetRecalculate) //If target has moved too far from last pos then recalculate the path
        {
            RecalculatePath();
        }

        //IF THIS PART WAS USED BY MOST OTHER AI THEN MOVE IT TO THE PARENT CLASS
        //if there is path with 2 points to move AND AI haven't reached the target
        if (path != null && path.corners.Length >= 2 && index <= path.corners.Length - 1) 
        {
            path.corners[index].z = 0f;
            movementS.direction = Vector3.ClampMagnitude(path.corners[index] - transform.position, 1);//move toward points on path

            float distance = (path.corners[index] - transform.position).magnitude;
            if (distance <= 0.5f) //check if target reached. If yes goes to the next point
            {
                //index = Mathf.Clamp(++index, 0, path.corners.Length - 1);
                index++;
            }
        }
        else
        {
            movementS.direction = Vector3.zero; //if there's no path or AI have reached target then don't move around
        }

        //for (int i = 0; i < path.corners.Length - 1; i++)
        //    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }
}
