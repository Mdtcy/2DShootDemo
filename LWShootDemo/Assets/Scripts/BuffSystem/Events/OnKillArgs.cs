using GameFramework;

namespace LWShootDemo.BuffSystem.Events
{
    public class OnKillArgs : DamageArgs
    {
        public static OnKillArgs Create(ref DamageInfo damageInfo)
        {
            var args = ReferencePool.Acquire<OnKillArgs>();
            args.DamageInfo = damageInfo;
            return args;
        }
    }
}