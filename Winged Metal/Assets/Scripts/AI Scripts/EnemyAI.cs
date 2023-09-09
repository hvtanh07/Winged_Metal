using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float targetOffsetRecalculate;
    public Transform target;
    private Vector3 targetLastPos;
    private NavMeshPath path;
    private float elapsed = 0.0f;
    private TankMovement movementS;
    private TankAttack attackS;
    private int index;
    // Start is called before the first frame update
    void Awake()
    {
        movementS = GetComponent<TankMovement>();
        attackS = GetComponentInChildren<TankAttack>();
        targetLastPos = target.position;
    }
    private void Start()
    {
        path = new NavMeshPath();
        index = 0;

        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        //var agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    void Update()
    {
        if ((targetLastPos - target.position).magnitude > targetOffsetRecalculate) //If target has moved too far from last pos then recalculate the path
        {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            targetLastPos = target.position;
            index = 0;
        }

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
            movementS.direction = Vector3.zero;
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }
}
