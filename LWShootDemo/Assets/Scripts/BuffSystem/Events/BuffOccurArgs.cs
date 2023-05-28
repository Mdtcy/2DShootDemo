using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
{
    public class BuffOccurArgs : BuffArgs
    {
        public int ModStack;
        
        public BuffOccurArgs(Buff buff, int modStack) : base(buff)
        {
            ModStack = modStack;
        }
    }
}