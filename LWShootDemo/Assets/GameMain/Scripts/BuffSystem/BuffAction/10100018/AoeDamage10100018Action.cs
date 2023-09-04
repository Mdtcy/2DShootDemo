using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("根据AoeCarrier有几层粘性炸弹造成伤害")]
    public class AoeDamage10100018Action : ActionBase<OnAoeCreateArgs, AoeDamage10100018ActionData>
    {
        protected override void ExecuteInternal(OnAoeCreateArgs args)
        {
            var caster = args.Aoe.Caster;
            float atk = caster.Atk;
            float damage = atk * Data.AtkPercent * (int)args.Aoe.Get("粘性爆炸层数");

            var side = caster.Side;
            foreach (var character in args.Aoe.ChaInAoe)
            {
                if (character.Side == side && !Data.HitAlly) continue;
                if (character.Side != side && !Data.HitFoe) continue;

                var dir = character.transform.position - args.Aoe.transform.position;
                GameEntry.Damage.DoDamage(caster,
                    character, (int) damage, dir, 0,
                    Data.DamageTag,
                    null);
            }
        }
    }
}