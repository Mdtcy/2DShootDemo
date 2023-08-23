using System.Collections.Generic;

namespace GameMain
{
    public class AoeDamage10100007Action : ActionBase<OnAoeCreateArgs, AoeDamage10100007ActionData>
    {
        protected override void ExecuteInternal(OnAoeCreateArgs args)
        {
            var caster = args.Aoe.Caster;

            var bindBuff = args.Aoe.RelatedBuff;
            float percent = bindBuff == null ? Data.BasePercent : Data.BasePercent + (bindBuff.Stack - 1) * Data.PercentPerStack;
            float damage = caster.Attack * percent;

            var side = caster.Side;
            foreach (var character in args.Aoe.ChaInAoe)
            {
                if(character.Side == side && !Data.HitAlly) continue;
                if(character.Side != side && !Data.HitFoe) continue;
                
                var dir = character.transform.position - args.Aoe.transform.position;
                GameEntry.Damage.DoDamage(caster, 
                    character, (int)damage, dir, 0, 
                    Data.DamageTag,
                    new List<AddBuffInfo>());
            }
        }
    }
}