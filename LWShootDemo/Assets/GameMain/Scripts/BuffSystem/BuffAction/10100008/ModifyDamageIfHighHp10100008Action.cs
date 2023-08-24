using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("如果目标血量高于指定值，修改伤害")]
    public class ModifyDamageIfHighHp10100008Action : ActionBase<OnHitArgs, ModifyDamageIfHighHp10100008ActionData>
    {
        protected override void ExecuteInternal(OnHitArgs args)
        {
            if((float)args.DamageInfo.defender.CurHp / args.DamageInfo.defender.MaxHp > Data.HpCanModify)
            {
                args.DamageInfo.Pct += (Data.BasePct + (Data.PctPerStack * (args.Buff.Stack - 1)));
            }
        }
    }
}