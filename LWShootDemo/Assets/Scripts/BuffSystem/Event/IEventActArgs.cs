using GameFramework;

namespace LWShootDemo.BuffSystem.Event
{
    public interface IEventActArgs : IReference
    {
    }
    
    // 所有Event的参数都继承自这个类
    public class BaseEventActArgs : IEventActArgs
    {
        public virtual void Clear()
        {
        }
    }
}