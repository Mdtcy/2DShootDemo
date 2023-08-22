using GameFramework;

namespace GameMain
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