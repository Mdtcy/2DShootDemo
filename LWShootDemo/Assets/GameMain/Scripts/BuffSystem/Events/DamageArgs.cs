using GameFramework;
using LWShootDemo.BuffSystem.Event;

namespace LWShootDemo.BuffSystem.Events
{
    public class DamageArgs : BaseBuffEventActArgs
    {
        public DamageInfo DamageInfo;

        // public static DamageArgs Create(ref DamageInfo damageInfo)
        // {
        //     var args = ReferencePool.Acquire<DamageArgs>();
        //     args.Init(ref damageInfo);
        //     return args;
        // }
    }
}