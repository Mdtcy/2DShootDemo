using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("移除时爆炸")]
    public class Boom10100018Action : ActionBase<BuffRemoveArgs, Boom10100018ActData>
    {
        protected override void ExecuteInternal(BuffRemoveArgs args)
        {
            var caster= args.Buff.Caster.GetComponent<Character>();
            var carrier = args.Buff.Carrier.GetComponent<Character>();
            var carrierPos = carrier.transform.position;
            var p = new Dictionary<string, object>();
            p.Add("粘性爆炸层数", args.Buff.Stack);
            GameEntry.Aoe.CreateAoe(Data.Aoe, carrierPos, Quaternion.identity, caster, 1, p);
        }
    }
}