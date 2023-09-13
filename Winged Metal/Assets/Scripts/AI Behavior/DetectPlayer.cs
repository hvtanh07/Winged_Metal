using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using BBUnity.Actions;

[Action("MyActions/DetectPlayer")]
[Help("Find player in the map to set parameters.")]
public class DetectPlayer : BasePrimitiveAction
{

    //[InParam("shootPoint")]
    //public Transform shootPoint;

    [OutParam("attackTarget")]
    private Transform target;

    public override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (target == null)
        {
            return TaskStatus.FAILED;
        }
        return TaskStatus.COMPLETED;
    }
}
