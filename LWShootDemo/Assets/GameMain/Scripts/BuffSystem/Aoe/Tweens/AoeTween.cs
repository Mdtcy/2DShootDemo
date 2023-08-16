using GameFramework;
using UnityEngine;

namespace GameMain
{
    public abstract class AoeTween : IReference
    {
        public AoeTweenData Data;
        public abstract Vector3 Tween(float timeElapsed, AoeState aoeState);

        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }
        
        public void Init(AoeTweenData data)
        {
            Data = data;
        }
        
        public abstract void Clear();
    }
}