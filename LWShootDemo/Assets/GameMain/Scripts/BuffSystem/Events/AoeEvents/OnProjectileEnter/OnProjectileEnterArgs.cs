using GameFramework;

namespace GameMain
{
    public class OnProjectileEnterArgs : BaseAoeEventActArgs
    {
        public Projectile Projectile;
        
        public static OnProjectileEnterArgs Create(Projectile projectile)
        {
            var args = ReferencePool.Acquire<OnProjectileEnterArgs>();
            args.Projectile = projectile;
            return args;
        }
    }
}