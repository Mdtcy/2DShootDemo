using Sirenix.OdinInspector;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    [LabelText("关联Buff(BindCasterBuffOnCreateAction)")]
    public class BindCasterBuffOnCreateAction : ActionBase<OnAoeCreateArgs, BindCasterBuffOnCreateActionData>
    {
        protected override void ExecuteInternal(OnAoeCreateArgs args)
        {
            Log.Debug("TryToBindCasterBuffOnCreateAction");
            // todo 优化
            var buff = args.Aoe.Caster.Buff.GetBuffById(Data.BuffData.ID);
            if (buff.Count != 1)
            {
                Log.Error("12");
            }

            Assert.IsTrue(buff.Count == 1);
            args.Aoe.BindBuff(buff[0]);
        }
    }
}