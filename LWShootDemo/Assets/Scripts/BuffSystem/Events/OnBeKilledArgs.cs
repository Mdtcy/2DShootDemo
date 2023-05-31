using GameFramework;

namespace LWShootDemo.BuffSystem.Events
{
    public class OnBeKilledArgs : DamageArgs
    {
        public static OnBeKilledArgs Create(ref DamageInfo damageInfo)
        {
            var args = ReferencePool.Acquire<OnBeKilledArgs>();
            args.DamageInfo = damageInfo;
            return args;
        }
    }
}