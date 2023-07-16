using GameFramework;

namespace LWShootDemo.BuffSystem.Events
{
    public class OnHitArgs : DamageArgs
    {
        public static OnHitArgs Create(ref DamageInfo damageInfo)
        {
            var hitArgs = ReferencePool.Acquire<OnHitArgs>();
            hitArgs.DamageInfo = damageInfo;
            return hitArgs;
        }
    }
}