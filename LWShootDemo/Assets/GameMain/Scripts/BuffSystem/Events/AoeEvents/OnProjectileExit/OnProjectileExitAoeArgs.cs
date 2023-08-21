using GameFramework;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("投射物离开AOE时")]
    public class OnProjectileExitAoeArgs : BaseAoeEventActArgs
    {
        public Projectile Projectile;
        
        public static OnProjectileExitAoeArgs Create(Projectile projectile)
        {
            var args = ReferencePool.Acquire<OnProjectileExitAoeArgs>();
            args.Projectile = projectile;
            return args;
        }
    }
}