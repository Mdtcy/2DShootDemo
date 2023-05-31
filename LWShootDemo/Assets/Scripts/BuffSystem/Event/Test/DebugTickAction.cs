using LWShootDemo.BuffSystem.Events;
using ModestTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    public class DebugTickActionData : ActionData<BaseEventActArgs, DebugTickAction>
    {
    }
    
    [LabelText("DebugTickAction")]
    public class DebugTickAction : ActionBase<BaseEventActArgs, DebugTickActionData>
    {
        protected override void ExecuteInternal(BaseEventActArgs args)
        {
            Debug.Log($"{args.Buff.ID} : {args.Buff.Ticked}次");
        }
    }
}