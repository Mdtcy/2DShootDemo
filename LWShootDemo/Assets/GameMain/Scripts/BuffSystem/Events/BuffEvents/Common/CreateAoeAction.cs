using UnityEngine;

namespace GameMain
{
    public class CreateAoeAction : ActionBase<BaseBuffEventActArgs, CreateAoeActionData>
    {
        protected override void ExecuteInternal(BaseBuffEventActArgs args)
        {
            var caster= args.Buff.Caster.GetComponent<Character>();
            var casterPos = caster.transform.position;
            GameEntry.Aoe.CreateAoe(Data.AoeProp, casterPos, Quaternion.identity, caster);
        }
    }
}