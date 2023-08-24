using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("修改伤害")]
    public class ModifyDamageAction : ActionBase<DamageArgs, ModifyDamageActionData>
    {
        protected override void ExecuteInternal(DamageArgs args)
        {
            args.DamageInfo.Add += Data.Add;
            args.DamageInfo.Pct += Data.Pct;
            args.DamageInfo.FinalAdd += Data.FinalAdd;
            args.DamageInfo.FinalPct += Data.FinalPct;
        }
    }
}