using System.Collections.Generic;
using Damages;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("根据AoeCarrier有几层粘性炸弹造成伤害")]
    public class AoeDamage10100018ActionData : ActionData<OnAoeCreateArgs, AoeDamage10100018Action> 
    {
        [Title("创造时造成伤害")]

        [LabelText("攻击力百分比")]
        public float AtkPercent;

        [LabelText("是否可以命中敌人")]
        public bool HitFoe;
        
        [LabelText("是否可以命中盟军")]
        public bool HitAlly;

        [LabelText("伤害标签")]
        public List<DamageInfoTag> DamageTag;
    }
}