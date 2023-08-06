using GameFramework;
using UnityEngine;

namespace GameMain
{
    public abstract class ProjectileGenericTweenData<T> : ProjectileTweenData where T : HomingProjectileTween1, new()
    {
        public override HomingProjectileTween1 CreateTween()
        {
            var tween = ReferencePool.Acquire<T>();
            tween.Init(this);
            return tween;
        }
    }
}