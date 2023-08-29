using GameFramework;

namespace GameMain
{
    public class BuffModStackArgs : BaseBuffEventActArgs
    {
        public int ModStack;
        
        public static BuffModStackArgs Create(int modStack)
        {
            var args = ReferencePool.Acquire<BuffModStackArgs>();
            args.ModStack = modStack;
            return args;
        }
    }
}