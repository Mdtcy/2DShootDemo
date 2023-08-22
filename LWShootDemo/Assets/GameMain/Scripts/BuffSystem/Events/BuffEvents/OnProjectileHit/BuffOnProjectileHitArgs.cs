using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class BuffOnProjectileHitArgs : BaseBuffEventActArgs
    {
        public Projectile Projectile;
        
        public Vector3 HitPoint;
        public GameObject HitObject;
        public Vector3 HitDirection;
        
        public static BuffOnProjectileHitArgs Create(Projectile projectile, GameObject hitObject, Vector3 hitPoint, Vector3 hitDirection)
        {
            var args = ReferencePool.Acquire<BuffOnProjectileHitArgs>();
            args.Projectile = projectile;
            args.HitObject = hitObject;
            args.HitPoint = hitPoint;
            args.HitDirection = hitDirection;
            return args;
        }
    }
}