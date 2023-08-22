using GameFramework;
namespace GameMain
{
    public class BuffOccurArgs : BaseBuffEventActArgs
    {
        public int ModStack;

        public static BuffOccurArgs Create(int modStack)
        {
            var args = ReferencePool.Acquire<BuffOccurArgs>();
            args.ModStack = modStack;
            return args;
        }
    }
}