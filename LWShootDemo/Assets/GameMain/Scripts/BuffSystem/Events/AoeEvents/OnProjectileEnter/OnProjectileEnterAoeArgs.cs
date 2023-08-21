using GameFramework;

namespace GameMain
{
    public class OnProjectileEnterAoeArgs : BaseAoeEventActArgs
    {
        public Projectile Projectile;
        
        public static OnProjectileEnterAoeArgs Create(Projectile projectile)
        {
            var args = ReferencePool.Acquire<OnProjectileEnterAoeArgs>();
            args.Projectile = projectile;
            return args;
        }
    }
}