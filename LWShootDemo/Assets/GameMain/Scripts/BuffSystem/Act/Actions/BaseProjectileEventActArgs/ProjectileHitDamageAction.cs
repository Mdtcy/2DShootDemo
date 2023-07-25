using LWShootDemo.BuffSystem.Event;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("子弹击中时造成伤害")]
    public class ProjectileHitDamageAction : ActionBase<OnProjectileHitArgs, ProjectileHitDamageActionData>
    {
        protected override void ExecuteInternal(OnProjectileHitArgs args)
        {
            
        }
    }
}