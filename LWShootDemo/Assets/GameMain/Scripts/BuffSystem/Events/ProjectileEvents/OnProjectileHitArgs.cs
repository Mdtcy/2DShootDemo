using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class OnProjectileHitArgs : BaseProjectileEventActArgs
    {
        public Vector3 HitPoint;
        public GameObject HitObject;
        public Vector3 HitDirection;
        
        public static OnProjectileHitArgs Create(GameObject hitObject, Vector3 hitPoint, Vector3 hitDirection)
        {
            var args = ReferencePool.Acquire<OnProjectileHitArgs>();
            args.HitObject = hitObject;
            args.HitPoint = hitPoint;
            args.HitDirection = hitDirection;
            return args;
        }
    }
}