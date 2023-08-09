using GameFramework;

namespace GameMain
{
    public class OnBeHurtArgs : DamageArgs
    {
        public static OnBeHurtArgs Create(ref DamageInfo damageInfo)
        {
            var args = ReferencePool.Acquire<OnBeHurtArgs>();
            args.DamageInfo = damageInfo;
            return args;
        }
    }
}