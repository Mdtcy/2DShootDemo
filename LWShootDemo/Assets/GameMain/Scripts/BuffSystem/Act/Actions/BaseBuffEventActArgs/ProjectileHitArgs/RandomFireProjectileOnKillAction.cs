using UnityEngine;

namespace GameMain
{
    public class RandomFireProjectileOnKillAction : ActionBase<OnKillArgs, RandomFireProjectileOnKillData>
    {
        protected override void ExecuteInternal(OnKillArgs args)
        {
            var carrier = args.Buff.Carrier.GetComponent<Character>();
            var firePoint = carrier.UnitBindManager.GetBindPointByKey("头顶发射导弹点").transform;

            int count = Data.BaseCount + Data.CountPerStackAdd * (args.Buff.Stack - 1);
            for (int i = 0; i < count; i++)
            {
                int totalAngle = Data.SectorTotalAngle;
                int intervalAngle = totalAngle / count;
                Quaternion rotation =
                    firePoint.rotation * Quaternion.Euler(0, 0, -totalAngle / 2 + intervalAngle * i);
                CreateProjectile(carrier, firePoint, rotation);
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