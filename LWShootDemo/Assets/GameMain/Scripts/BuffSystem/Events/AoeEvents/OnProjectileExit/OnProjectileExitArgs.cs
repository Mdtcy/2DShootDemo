using GameFramework;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("投射物离开AOE时")]
    public class OnProjectileExitArgs : BaseAoeEventActArgs
    {
        public Projectile Projectile;
        
        public static OnProjectileExitArgs Create(Projectile projectile)
        {
            var args = ReferencePool.Acquire<OnProjectileExitArgs>();
            args.Projectile = projectile;
            return args;
        }
    }
}