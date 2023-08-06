using UnityEngine;

namespace GameMain
{
    public class ForwardTween1 : HomingProjectileTween1
    {
        public override Vector3 Tween(float timeElapsed, Projectile projectile, Transform followTarget = null)
        {
            return projectile.Forward * projectile.Speed;
        }

        public override void Clear()
        {
        }
    }
}