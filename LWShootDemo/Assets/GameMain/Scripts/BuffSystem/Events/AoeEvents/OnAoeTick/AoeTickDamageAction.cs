using System.Collections.Generic;
using Damages;

namespace GameMain
{
    public class AoeTickDamageAction : ActionBase<OnAoeTickArgs, AoeTickDamageActionData>
    {
        protected override void ExecuteInternal(OnAoeTickArgs args)
        {
            var caster = args.Aoe.Caster;
            float damage = caster.Attack * Data.BasePercent;

            foreach (var character in args.Aoe.ChaInAoe)
            {
                var dir = character.transform.position - args.Aoe.transform.position;
                GameEntry.Damage.DoDamage(caster, 
                    character, (int)damage, dir, 0, 
                    new List<DamageInfoTag>(),
                    new List<AddBuffInfo>());
            }
        }
    }
}