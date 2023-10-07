using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : AI
{
    public Collider2D patrolArea;
    
    private void Start()
    {
        InitParameters();
        BT.tree.blackboard.randomArea = patrolArea;
    }
    

    void Update()
    {
        if (BT.tree.blackboard.target == null) return;

        if (targetLastPos != BT.tree.blackboard.target) //If target has moved too far from last pos then recalculate the path
        {
            RecalculatePath();
        }

        //if there is path with 2 points to move AND AI haven't reached the target
        if (path != null && path.corners.Length >= 2 && index <= path.corners.Length - 1)
        {
            path.corners[index].z = 0f;
            vehicle.ID.events.OnMovementDirectionChange?.Invoke(Vector3.ClampMagnitude(path.corners[index] - transform.position, 1));
            float distance = (path.corners[index] - transform.position).magnitude;
            if (distance <= 0.5f) //check if target reached. If yes goes to the next point
            {
                index++;
            }
        }
        else
        {
            vehicle.ID.events.OnMovementDirectionChange?.Invoke(Vector2.zero);
        }
    }
}
