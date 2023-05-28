using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
{
    public class BuffArgs : BaseEventActArgs
    {
        public Buff Buff;

        public BuffArgs(Buff buff)
        {
            Buff = buff;
        }
    }
}