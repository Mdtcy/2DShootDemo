using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("DebugTickAction")]
    public class DebugTickAction : ActionBase<BaseBuffEventActArgs, DebugTickActionData>
    {
        protected override void ExecuteInternal(BaseBuffEventActArgs args)
        {
            Debug.Log($"{args.Buff.ID} : {args.Buff.Ticked}次");
        }
    }
}