using GameFramework;

namespace GameMain
{
    public class OnAoeCreateArgs : BaseAoeEventActArgs
    {
        public static OnAoeCreateArgs Create()
        {
            var args = ReferencePool.Acquire<OnAoeCreateArgs>();
            return args;
        }
    }
}