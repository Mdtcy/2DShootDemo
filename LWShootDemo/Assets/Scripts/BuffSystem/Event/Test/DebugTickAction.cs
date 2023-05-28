using LWShootDemo.BuffSystem.Events;
using ModestTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    public class DebugTickActionData : ActionData<BuffArgs, DebugTickAction>
    {
    }
    
    [LabelText("DebugTickAction")]
    public class DebugTickAction : ActionBase<BuffArgs, DebugTickActionData>
    {
        protected override void ExecuteInternal(BuffArgs args)
        {
            Debug.Log($"{args.Buff.Name} : {args.Buff.Ticked}次");
        }
    }
}