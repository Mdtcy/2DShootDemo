using GameFramework;

namespace GameMain
{
    public class OnProjectileRemoveArgs : BaseProjectileEventActArgs
    {
        public static OnProjectileRemoveArgs Create()
        {
            var args = ReferencePool.Acquire<OnProjectileRemoveArgs>();
            return args;
        }
    }
}