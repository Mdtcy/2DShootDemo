using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("修改伤害")]
    public class ModifyDamageActionData : ActionData<DamageArgs,ModifyDamageAction>
    {
        public int Add;
        public int Pct;
        public int FinalAdd;
        public int FinalPct;
    }
}