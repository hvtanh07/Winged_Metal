using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Basic/SetTransform")]
    [Help("Sets a value to a Transform variable")]
    public class SetTransform : BasePrimitiveAction
    {
        ///<value>OutPut Vector3D Parameter.</value>
        [OutParam("var")]
        [Help("output variable")]
        public Transform var;
        
        ///<value>Input Vector3D Parameter.</value>
        [InParam("value")]
        [Help("Value")]
        public Transform value;

        /// <summary>Initialization Method of SetVector3.</summary>
        /// <remarks>Initializes the value of a Vector3D.</remarks>
        public override void OnStart()
        {
            var = value;
        }


        /// <summary>Method of Update of SetVector3.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
