using Sirenix.OdinInspector;

namespace GameMain
{
    public class AoeTickDamageActionData : ActionData<OnAoeTickArgs, AoeTickDamageAction>
    {
        [Title("每秒造成攻击力百分比伤害的Aoe")]
        
        [LabelText("基础伤害百分比")]
        public float BasePercent;
        
        [LabelText("是否可以命中敌人")]
        public bool HitFoe;
        
        [LabelText("是否可以命中盟军")]
        public bool HitAlly;
        
        // [LabelText("每层叠加的伤害百分比")]
        // public float PercentPerStack;
    }
}