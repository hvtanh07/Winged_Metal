using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : AIParent
{
    private TankMovement movementS;
    private TankAttack attackS;
    private TankResources resourcesS;
    public BehaviourTreeRunner behaviourTree;
    //private bool openFire;

    // Start is called before the first frame update
    void Awake()
    {
        movementS = GetComponent<TankMovement>();
        attackS = GetComponentInChildren<TankAttack>();
        resourcesS = GetComponent<TankResources>();
        behaviourTree = GetComponent<BehaviourTreeRunner>();
    }

    private void Start()
    {

        InitParameters();
        behaviourTree.tree.blackboard.movement = movementS;
        behaviourTree.tree.blackboard.attack = attackS;
        behaviourTree.tree.blackboard.resources = resourcesS;
        //ONLY USE WHEN SWITCHING TO NAVMESH AGENT COMPONENT
        //var agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Bullet")) return;

        BulletScript bullet = col.gameObject.GetComponent<BulletScript>();
        if (bullet == null) return;

        if(bullet.bulletOwner != TankAttack.BulletOwner.enemy){
            resourcesS.TakeDamage(bullet.GetDamage());
            col.gameObject.SetActive(false);
        }
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


        //Update BT data
        behaviour.tree.blackboard.currentEn = GetComponent<TankResources>().GetCurrentEn();
    }
    public void Dash()
    {
        if (movementS.IsAbleToDash())
        {
            StartCoroutine(movementS.DashToggle());
            RecalculatePath();
        }
    }
    public void Dash(Vector2 dashDirection)
    {
        if (movementS.IsAbleToDash())
        {
            StartCoroutine(movementS.DashToggle(dashDirection));
            RecalculatePath();
        }
    }
}
