using GameFramework;

namespace GameMain
{
    public class BuffRemoveArgs : BaseBuffEventActArgs
    {
        public static BuffRemoveArgs Create()
        {
            return ReferencePool.Acquire<BuffRemoveArgs>();
        }
    }
}