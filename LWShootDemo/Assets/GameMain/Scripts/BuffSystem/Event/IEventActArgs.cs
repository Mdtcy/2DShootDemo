using GameFramework;
using GameMain;

namespace GameMain
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
    public class BaseProjectileEventActArgs : IEventActArgs
    {
        public void Clear()
        {
            
        }
        
        public Projectile Projectile { get; set; }
    }
}