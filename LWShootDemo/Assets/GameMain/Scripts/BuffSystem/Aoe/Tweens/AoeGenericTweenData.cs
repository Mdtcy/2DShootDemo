using GameFramework;

namespace GameMain
{
    public class AoeTweenData<T> : AoeTweenData where T : AoeTween , new()
    {
        public override AoeTween CreateTween()
        {
            var tween = ReferencePool.Acquire<T>();
            tween.Init(this);
            return tween;
        }
    }
}