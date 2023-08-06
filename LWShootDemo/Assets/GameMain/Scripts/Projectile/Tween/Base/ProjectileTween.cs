using GameFramework;
using UnityEngine;

namespace GameMain
{
    public abstract class ProjectileTween : IReference
    {
        public ProjectileTweenData Data;
        public abstract Vector3 Tween(float timeElapsed, Projectile projectile, Transform followTarget = null);

        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }
        
        public void Init(ProjectileTweenData data)
        {
            Data = data;
        }
        
        public abstract void Clear();
    }
}