using UnityEngine;

namespace GameMain
{
    public class CreateAoe10100007Action : ActionBase<BuffOnProjectileHitArgs, CreateAoe10100007ActionData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            var hitPos = args.HitPoint;
            var carrier= args.Buff.Carrier.GetComponent<Character>();
            GameEntry.Aoe.CreateAoe(Data.AoeProp, hitPos, Quaternion.identity, carrier, Data.Radius);
        }
    }
}