using GameFramework;
using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
{
    public class BuffRemoveArgs : BaseEventActArgs
    {
        public static BuffRemoveArgs Create()
        {
            return ReferencePool.Acquire<BuffRemoveArgs>();
        }
    }
}