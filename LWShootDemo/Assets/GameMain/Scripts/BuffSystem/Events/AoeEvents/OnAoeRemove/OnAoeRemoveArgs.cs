using GameFramework;

namespace GameMain
{
    public class OnAoeRemoveArgs : BaseAoeEventActArgs
    {
        public static OnAoeRemoveArgs Create()
        {
            var args = ReferencePool.Acquire<OnAoeRemoveArgs>();
            return args;
        }
    }
}