using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyAI : AI
{
    private VehicleResources resourcesS;
    //private bool openFire;

    // Start is called before the first frame update
    private void OnEnable()
    {
        vehicle.ID.events.OnEnUpdate += updateBTEn;
        vehicle.ID.events.OnBeingHit += OnBeingHit;
        vehicle.ID.events.OnDashComplete += OnDashComplete;
    }
    private void Start()
    {
        InitParameters();
        behaviour = GetComponent<BehaviourTreeRunner>();
        behaviour.tree.blackboard.ai = this;
    }

    public void ConsumeEnergy(int amountEnergy)
    {
        resourcesS.ConsumeEnergy(amountEnergy);
    }
    public void OnDashComplete()
    {
        RecalculatePath();
    }


    void Update()
    {
        if (behaviour.tree.blackboard.target == null) return;

        if (targetLastPos != behaviour.tree.blackboard.target) //If target has moved too far from last pos then recalculate the path
        {
            RecalculatePath();
        }

        //IF THIS PART WAS USED BY MOST OTHER AI THEN MOVE IT TO THE PARENT CLASS
        //if there is path with 2 points to move AND AI haven't reached the target
        if (path != null && path.corners.Length >= 2 && index <= path.corners.Length - 1)
        {
            path.corners[index].z = 0f;
            vehicle.ID.events.OnDirectionChange?.Invoke(Vector3.ClampMagnitude(path.corners[index] - transform.position, 1));//move toward points on path
            float distance = (path.corners[index] - transform.position).magnitude;
            if (distance <= 0.5f) //check if target reached. If yes goes to the next point
            {
                //index = Mathf.Clamp(++index, 0, path.corners.Length - 1);
                index++;
            }
        }
        else
        {
            vehicle.ID.events.OnDirectionChange?.Invoke(Vector2.zero);//if there's no path or AI have reached target then don't move around
        }
    }


}
