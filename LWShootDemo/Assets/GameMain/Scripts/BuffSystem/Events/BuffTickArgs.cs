using GameFramework;
using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
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