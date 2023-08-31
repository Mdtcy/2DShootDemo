using GameFramework;

namespace GameMain
{
    public class BuffOnProjectileCreateArgs : BaseBuffEventActArgs
    {
        public Projectile Projectile;
        
        public static BuffOnProjectileCreateArgs Create(Projectile projectile)
        {
            var args = ReferencePool.Acquire<BuffOnProjectileCreateArgs>();
            args.Projectile = projectile;
            return args;
        }
    }
}