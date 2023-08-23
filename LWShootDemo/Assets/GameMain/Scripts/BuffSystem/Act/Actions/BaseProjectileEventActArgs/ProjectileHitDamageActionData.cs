
using Sirenix.OdinInspector;

namespace GameMain
{
    public class ProjectileHitDamageActionData : ActionData<OnProjectileHitArgs, ProjectileHitDamageAction>
    {
        [LabelText("造成攻击力的百分比伤害")]
        public float DamagePercent;
    }
}