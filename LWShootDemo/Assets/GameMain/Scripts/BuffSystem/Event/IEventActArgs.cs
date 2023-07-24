using GameFramework;
using GameMain;

namespace LWShootDemo.BuffSystem.Event
{
    public interface IEventActArgs : IReference
    {
    }
    
    // 所有Event的参数都继承自这个类
    public class BaseBuffEventActArgs : IEventActArgs
    {
        public virtual void Clear()
        {
        }

        public Buff Buff { get; set; }
    }
    
    // 所有ProjectileEvent的参数都继承自这个类
    public class BaseProjectileEventActArgs : BaseBuffEventActArgs
    {
        public Projectile Projectile { get; set; }
    }
}