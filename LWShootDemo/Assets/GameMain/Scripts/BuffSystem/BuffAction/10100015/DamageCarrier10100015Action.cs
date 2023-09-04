using System.Collections.Generic;
using Damages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("伤害持有者10100015")]
    public class DamageCarrier10100015Action : ActionBase<BuffTickArgs, DamageCarrier10100015ActData>
    {
        protected override void ExecuteInternal(BuffTickArgs args)
        {
            var caster = args.Buff.Caster.GetComponent<Character>();
            int damage = (int) (caster.Atk * Data.DamageAtkPercent);
            GameEntry.Damage.DoDamage(caster, 
                args.Buff.Carrier.GetComponent<Character>(), damage, Vector3.zero, 0, 
                Data.DamageInfoTags,
                new List<AddBuffInfo>());
        }
    }
}