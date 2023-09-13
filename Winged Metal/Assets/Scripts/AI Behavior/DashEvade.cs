using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using BBUnity.Actions;

[Action("MyActions/DashEvade")]
[Help("Dash from danger.")]
public class DashEvade : BasePrimitiveAction
{
    //[OutParam("attackTarget")]
    //private Transform target;
    [InParam("aiControler")]
    public EnemyAI aiControler;

    public override TaskStatus OnUpdate()
    {
        aiControler.DashEvade(Vector2.up);
        return TaskStatus.COMPLETED;
    }
}
