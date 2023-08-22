using GameFramework;

namespace GameMain
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