using GameFramework;
using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
{
    public class BuffOccurArgs : BaseEventActArgs
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