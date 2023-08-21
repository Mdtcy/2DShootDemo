using GameFramework;

namespace GameMain
{
    public class OnAoeTickArgs : BaseAoeEventActArgs
    {
        // Tick的次数 是第几次
        public int TickTime;
        
        public static OnAoeTickArgs Create(int tickTime)
        {
            var args = ReferencePool.Acquire<OnAoeTickArgs>();
            args.TickTime = tickTime;
            return args;
        }
    }
}