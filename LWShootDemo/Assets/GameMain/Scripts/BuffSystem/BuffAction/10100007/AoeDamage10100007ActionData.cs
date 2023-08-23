using System.Collections.Generic;
using Damages;
using Sirenix.OdinInspector;

namespace GameMain
{
    public class AoeDamage10100007ActionData :  ActionData<OnAoeCreateArgs, AoeDamage10100007Action> 
    {
        [Title("创造时造成伤害")]
        
        [LabelText("基础伤害百分比")]
        public float BasePercent;
        
        [LabelText("每层Buff叠加的伤害百分比")]
        public float PercentPerStack;

        [LabelText("是否可以命中敌人")]
        public bool HitFoe;
        
        [LabelText("是否可以命中盟军")]
        public bool HitAlly;

        [LabelText("伤害标签")]
        public List<DamageInfoTag> DamageTag;
    }
}