using UnityEngine;

namespace GameMain
{
    public class RandomFireProjectileAction : ActionBase<BuffOnProjectileHitArgs, RandomFireProjectileActData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            if (args.Projectile.ContainTag(Data.ProjectileTag) && // 子弹包含标签
                Random.Range(0,1) <= Data.ProbabilityPerStack * args.Buff.Stack)// 概率达到
            {
                var caster = args.Buff.Caster.GetComponent<Character>();
                var firePoint = caster.UnitBindManager.GetBindPointByKey("头顶发射导弹点").transform;
                var randomRotation = 
                    Quaternion.Euler(0, 0, Random.Range(Data.RandomFireRotationRange.x, Data.RandomFireRotationRange.y));
                GameEntry.Projectile.CreateProjectile(Data.ProjectileProp,
                    caster,
                    firePoint.position,
                    firePoint.rotation * randomRotation);
            }
        }
    }
}