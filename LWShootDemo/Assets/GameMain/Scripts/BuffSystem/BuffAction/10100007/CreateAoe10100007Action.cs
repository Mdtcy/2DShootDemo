using UnityEngine;

namespace GameMain
{
    public class CreateAoe10100007Action : ActionBase<BuffOnProjectileHitArgs, CreateAoe10100007ActionData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            var hitPos = args.HitPoint;
            var caster= args.Buff.Caster.GetComponent<Character>();
            GameEntry.Aoe.CreateAoe(Data.AoeProp, hitPos, Quaternion.identity, caster, Data.Radius);
        }
    }
}