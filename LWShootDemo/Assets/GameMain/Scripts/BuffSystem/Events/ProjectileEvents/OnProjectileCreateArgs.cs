using GameFramework;
using LWShootDemo.BuffSystem.Event;

namespace GameMain
{
    public class OnProjectileCreateActArgs : BaseProjectileEventActArgs
    {
        public static OnProjectileCreateActArgs Create()
        {
            var args = ReferencePool.Acquire<OnProjectileCreateActArgs>();
            return args;
        }
    }
}