using System.Collections.Generic;
using Damages;

namespace GameMain
{
    public class AoeTickDamageAction : ActionBase<OnAoeTickArgs, AoeTickDamageActionData>
    {
        protected override void ExecuteInternal(OnAoeTickArgs args)
        {
            var caster = args.Aoe.Caster;

            var bindBuff = args.Aoe.RelatedBuff;
            float percent = bindBuff == null ? Data.BasePercent : Data.BasePercent + (bindBuff.Stack - 1) * Data.PercentPerStack;
            float damage = caster.Atk * percent;

            var side = caster.Side;
            foreach (var character in args.Aoe.ChaInAoe)
            {
                if(character.Side == side && !Data.HitAlly) continue;
                if(character.Side != side && !Data.HitFoe) continue;
                
                var dir = character.transform.position - args.Aoe.transform.position;
                GameEntry.Damage.DoDamage(caster, 
                    character, (int)damage, dir, 0, 
                    new List<DamageInfoTag>(),
                    new List<AddBuffInfo>());
            }
        }
    }
}