using System.Collections.Generic;
using Damages;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("子弹击中时造成伤害")]
    public class ProjectileHitDamageAction : ActionBase<OnProjectileHitArgs, ProjectileHitDamageActionData>
    {
        protected override void ExecuteInternal(OnProjectileHitArgs args)
        {
            var caster = args.Projectile.Caster;
            var target = args.HitObject.GetComponent<Character>();
            
            // todo 现在只伤害敌人
            if (target == null)
            {
                return;
            }

            int damage = (int) (caster.Attack * Data.DamagePercent);
            var dir = args.HitDirection;
            GameEntry.Damage.DoDamage(caster, 
                target, damage, dir, 0, 
                new List<DamageInfoTag>(),
                null);
        }
    }
}