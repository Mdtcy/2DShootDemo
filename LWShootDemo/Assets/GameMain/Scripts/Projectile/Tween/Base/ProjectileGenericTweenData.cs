using GameFramework;
using UnityEngine;

namespace GameMain
{
    public abstract class ProjectileGenericTweenData<T> : ProjectileTweenData where T : ProjectileTween, new()
    {
        public override ProjectileTween CreateTween()
        {
            var tween = ReferencePool.Acquire<T>();
            tween.Init(this);
            return tween;
        }
    }
}