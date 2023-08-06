using GameFramework;
using LWShootDemo.BuffSystem.Event;
using UnityEngine;

namespace GameMain
{
    public class OnProjectileHitArgs : BaseProjectileEventActArgs
    {
        public static OnProjectileHitArgs Create()
        {
            var args = ReferencePool.Acquire<OnProjectileHitArgs>();
            // args.HitObject = hitObject;
            // args.HitPoint = hitPoint;
            // args.HitNormal = hitNormal;
            return args;
        }
    }
}