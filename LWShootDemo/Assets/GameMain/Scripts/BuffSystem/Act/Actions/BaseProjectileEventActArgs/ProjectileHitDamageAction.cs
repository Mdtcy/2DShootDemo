using System.Collections.Generic;
using Damages;
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.BuffSystem.Event;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("子弹击中时造成伤害")]
    public class ProjectileHitDamageAction : ActionBase<OnProjectileHitArgs, ProjectileHitDamageActionData>
    {
        protected override void ExecuteInternal(OnProjectileHitArgs args)
        {
            var caster = args.Projectile.caster;
            var target = args.HitObject.GetComponent<Character>();
            int damage = Data.Damage;
            var dir = args.HitDirection;
            GameEntry.Damage.DoDamage(caster, 
                target, damage, dir, 0, 
                new List<DamageInfoTag>(),
                new List<AddBuffInfo>());
        }
    }
}