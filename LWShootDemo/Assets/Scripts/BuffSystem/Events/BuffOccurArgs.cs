using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
{
    public class BuffOccurArgs : BuffArgs
    {
        public int ModStack;
        
        public BuffOccurArgs(int modStack)
        {
            ModStack = modStack;
        }
    }
}