using GameFramework;

namespace GameMain
{
    public class BuffTickArgs : BaseBuffEventActArgs
    {
        public static BuffTickArgs Create()
        {
            var args = ReferencePool.Acquire<BuffTickArgs>();
            return args;
        }
    }
}