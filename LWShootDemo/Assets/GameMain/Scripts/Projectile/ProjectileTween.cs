using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class ProjectileTween : IReference
    {
        public void Clear()
        {
        }

        public virtual Vector3 Tween(float timeElapsed, Projectile projectile, Transform followTarget = null)
        {
            return Vector3.zero;
        }
        
        
        // public static ProjectileTween Create()
        // {
        //     var projectileTween = ReferencePool.Acquire<ProjectileTween>();
        //     return projectileTween;
        // }

        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }
    }
}