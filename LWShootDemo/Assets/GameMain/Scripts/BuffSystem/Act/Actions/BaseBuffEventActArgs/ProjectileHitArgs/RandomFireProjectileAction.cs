using UnityEngine;

namespace GameMain
{
    public class RandomFireProjectileAction : ActionBase<BuffOnProjectileHitArgs, RandomFireProjectileActData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            if (args.Projectile.ContainTag(Data.ProjectileTag) && // 子弹包含标签
                Random.Range(0f,1f) <= Data.ProbabilityPerStack * args.Buff.Stack)// 概率达到
            {
                var carrier = args.Buff.Carrier.GetComponent<Character>();
                var firePoint = carrier.UnitBindManager.GetBindPointByKey("头顶发射导弹点").transform;

                for (int i = 0; i < Data.Count; i++)
                {
                    int totalAngle = Data.SectorAngle * (Data.Count - 1);
                    Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, -totalAngle / 2 + Data.SectorAngle * i);   
                    var randomRotation = 
                        Quaternion.Euler(0, 0, Random.Range(Data.RandomFireRotationRange.x, Data.RandomFireRotationRange.y));
                    rotation = rotation * randomRotation;
                    CreateProjectile(carrier, firePoint, rotation);
                }
            }
        }
        
        private void CreateProjectile(Character carrier, Transform firePoint , Quaternion rotation)
        {
            GameEntry.Projectile.CreateProjectile(Data.ProjectileProp,
                carrier,
                firePoint.position,
                firePoint.rotation * rotation);
        }
    }
}